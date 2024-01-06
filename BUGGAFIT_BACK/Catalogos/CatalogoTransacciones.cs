using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoTransacciones : ICatalogoTransacciones
    {
        private readonly MyDBContext myDbContext;
        private readonly CatalogoCompras catalogoCompras;
        private readonly CatalogoVentas catalogoVentas;
        private readonly CatalogoGastos catalogoGastos;

        public CatalogoTransacciones(MyDBContext myDbContext)
        {
            this.myDbContext = myDbContext;
            this.catalogoCompras = new(myDbContext);
            this.catalogoVentas = new(myDbContext);
            this.catalogoGastos = new(myDbContext);
        }

        public async Task<ResponseObject> ActualizarTrasaccionAsync(Transacciones transaccion)
        {
            ArgumentNullException.ThrowIfNull(transaccion.TRA_CODIGO, nameof(transaccion));
            try
            {
                TRANSACCIONES _transaccion = new()
                {
                    TIC_CODIGO = transaccion.TIC_CODIGO,
                    TIC_CUENTA = transaccion.TIC_CUENTA,
                    TRA_TIPO = transaccion.TRA_TIPO,
                    TRA_FECHACREACION = DateTime.Now,
                    //TRA_CONFIRMADA = transaccion.TRA_CONFIRMADA,
                    //TRA_ESTADO = transaccion.TRA_ESTADO,
                    //TRA_FECHACONFIRMACION = DateTime.Now,
                    //TRA_CODIGOENLACE = transaccion.TRA_CODIGOENLACE,
                    //TRA_FUEANULADA = transaccion.TRA_FUEANULADA,
                    TRA_NUMEROTRANSACCIONBANCO = transaccion.TRA_NUMEROTRANSACCIONBANCO,
                    USU_CEDULA_CONFIRMADOR = transaccion.USU_CEDULA_CONFIRMADOR,
                    //TRA_VALOR = transaccion.TRA_VALOR
                };
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteTrasaccion(transaccion.TRA_CODIGO))
                    throw new Exception($"La Transaccion con el codigo {transaccion.TRA_CODIGO} no existe.");

                throw;
            }
            catch (Exception) { throw; }
            return ResponseClass.Response(statusCode: 204, message: $"Transaccion Actualizada Exitosamente.");
        }
        public async Task<ResponseObject> AnularTrasaccionesAsync(int Id)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.FindAsync(Id) ?? throw new Exception($"La transaccion con el codigo {Id} no existe.");
                _transaccion.TRA_FUEANULADA = true;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;

                var tipoTransaccion = TiposTransacciones.GetTipoDevolucion(_transaccion.TRA_TIPO);
                await CrearTrasaccionAsync(new()
                {
                    TIC_CUENTA = _transaccion.TIC_CODIGO,
                    TIC_CODIGO = _transaccion.TIC_CODIGO,
                    TRA_TIPO = tipoTransaccion.Nombre,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = true,
                    TRA_ESTADO = true,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = _transaccion.TRA_CODIGOENLACE,
                    TRA_FUEANULADA = false,
                    TRA_NUMEROTRANSACCIONBANCO = 0,
                    USU_CEDULA_CONFIRMADOR = _transaccion.USU_CEDULA_CONFIRMADOR,
                    TRA_VALOR = tipoTransaccion.EsRetiroDeDinero ? -(_transaccion.TRA_VALOR) : _transaccion.TRA_VALOR,
                });

                await myDbContext.SaveChangesAsync();
                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Anulada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> AnularTrasaccionesPorIdEnlaceAsync(string idEnlace)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.Where(x => x.TRA_CODIGOENLACE == idEnlace).FirstOrDefaultAsync()
                    ?? throw new NullReferenceException($"La transaccion con el codigo de enlace {idEnlace} no existe.");
                _transaccion.TRA_FUEANULADA = false;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;

                var tipoTransaccion = TiposTransacciones.GetTipoDevolucion(_transaccion.TRA_TIPO);
                await CrearTrasaccionAsync(new()
                {
                    TIC_CUENTA = _transaccion.TIC_CODIGO,
                    TIC_CODIGO = _transaccion.TIC_CODIGO,
                    TRA_TIPO = tipoTransaccion.Nombre,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = true,
                    TRA_ESTADO = true,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = _transaccion.TRA_CODIGO.ToString(),
                    TRA_FUEANULADA = false,
                    TRA_NUMEROTRANSACCIONBANCO = 0,
                    USU_CEDULA_CONFIRMADOR = _transaccion.USU_CEDULA_CONFIRMADOR,
                    TRA_VALOR = tipoTransaccion.EsRetiroDeDinero ? -(_transaccion.TRA_VALOR) : _transaccion.TRA_VALOR,
                });
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Anulada Exitosamente.");
            }
            catch (NullReferenceException ex)
            {
                return ResponseClass.Response(statusCode: 500, message: ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> BorrarTrasaccionAsync(int Id)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.FindAsync(Id) ?? throw new Exception($"La transaccion con el codigo {Id} no existe.");
                if (_transaccion.TRA_CONFIRMADA == true)
                    throw new Exception($"La transaccion con el codigo {Id} no se puede eliminar. Transaccion Confirmada.");

                _transaccion.TRA_ESTADO = false;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                await RetornarDineroCuentas(idCuenta: _transaccion.TIC_CODIGO, cantidadDinero: _transaccion.TRA_VALOR ?? 0);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> BorrarTrasaccionPorIdEnlaceAsync(string idEnlace, TiposTransacciones tiposTransacciones)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.Where(x => x.TRA_CODIGOENLACE == idEnlace && x.TRA_TIPO == tiposTransacciones.Nombre).FirstOrDefaultAsync()
                    ?? throw new NullReferenceException($"La transaccion con el codigo de enlace {idEnlace} no existe.");
                if (_transaccion.TRA_CONFIRMADA == true)
                    throw new Exception($"La transaccion con el codigo {idEnlace} no se puede eliminar. Transaccion Confirmada.");

                _transaccion.TRA_ESTADO = false;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                //await RetornarDineroCuentas(idCuenta: _transaccion.TIC_CODIGO, cantidadDinero: _transaccion.TRA_VALOR ?? 0); si no esta confirmada no debe realizar movimientos de dinero
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Eliminada Exitosamente.");
            }
            catch (NullReferenceException ex)
            {
                return ResponseClass.Response(statusCode: 500, message: ex.Message);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ConfirmarTrasaccionAsync(int Id, string usurioConfirmador)
        {
            try
            {
                var transaccionPrincipal = await ConfirmarTransacciones(Id, usurioConfirmador);
                if (transaccionPrincipal.Item2 is not null && transaccionPrincipal.Item2.TRA_TIPO == TiposTransacciones.VENTA.Nombre)
                {

                    int idGasto = await (from t in myDbContext.TRANSACCIONES
                                         join v in myDbContext.VENTAS on t.TRA_CODIGOENLACE equals v.VEN_CODIGO.ToString()
                                         join g in myDbContext.GASTOS on v.VEN_CODIGO equals g.VEN_CODIGO
                                         where t.TRA_CODIGO == transaccionPrincipal.Item2.TRA_CODIGO
                                         select g.GAS_CODIGO).FirstOrDefaultAsync();
                    int _idTransaccionGasto = await myDbContext.TRANSACCIONES
                        .Where(x => x.TRA_CODIGOENLACE == idGasto.ToString() && x.TRA_TIPO == TiposTransacciones.COSTOENVIO.Nombre)
                        .Select(x => x.TRA_CODIGO)
                        .FirstOrDefaultAsync();
                    if (_idTransaccionGasto != 0)
                    {
                        await ConfirmarTransacciones(_idTransaccionGasto, usurioConfirmador);
                    }
                }
                return transaccionPrincipal.Item1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ConfirmarTrasaccionesAsync(List<int> idTransacciones, string usurioConfirmador)
        {
            try
            {
                foreach (int id in idTransacciones)
                {
                    var transaccionProncipal = await ConfirmarTransacciones(id, usurioConfirmador);
                    if (transaccionProncipal.Item2 is not null && transaccionProncipal.Item2.TRA_TIPO == TiposTransacciones.VENTA.Nombre)
                    {

                        int idGasto = await (from t in myDbContext.TRANSACCIONES
                                             join v in myDbContext.VENTAS on t.TRA_CODIGOENLACE equals v.VEN_CODIGO.ToString()
                                             join g in myDbContext.GASTOS on v.VEN_CODIGO equals g.VEN_CODIGO
                                             where t.TRA_CODIGO == transaccionProncipal.Item2.TRA_CODIGO
                                             select g.GAS_CODIGO).FirstOrDefaultAsync();
                        int _idTransaccionGasto = await myDbContext.TRANSACCIONES
                            .Where(x => x.TRA_CODIGOENLACE == idGasto.ToString() && x.TRA_TIPO == TiposTransacciones.COSTOENVIO.Nombre)
                            .Select(x => x.TRA_CODIGO)
                            .FirstOrDefaultAsync();
                        if (_idTransaccionGasto != 0)
                        {
                            await ConfirmarTransacciones(_idTransaccionGasto, usurioConfirmador);
                        }
                    }
                }
                return ResponseClass.Response(statusCode: 204, message: $"Transacciones Confirmadas Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> CrearTrasaccionAsync(Transacciones transaccion)
        {
            try
            {
                TRANSACCIONES _transaccion = new()
                {
                    TIC_CODIGO = transaccion.TIC_CODIGO,
                    TIC_CUENTA = transaccion.TIC_CUENTA,
                    TRA_TIPO = transaccion.TRA_TIPO,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = transaccion.TRA_CONFIRMADA,
                    TRA_ESTADO = transaccion.TRA_ESTADO,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = transaccion.TRA_CODIGOENLACE,
                    TRA_FUEANULADA = transaccion.TRA_FUEANULADA,
                    TRA_NUMEROTRANSACCIONBANCO = transaccion.TRA_NUMEROTRANSACCIONBANCO,
                    USU_CEDULA_CONFIRMADOR = transaccion.USU_CEDULA_CONFIRMADOR,
                    TRA_VALOR = transaccion.TRA_VALOR
                };
                myDbContext.TRANSACCIONES.Add(_transaccion);
                if (transaccion.TRA_CONFIRMADA == true)
                {
                    await AgregarDineroCuentas(idCuenta: _transaccion.TIC_CODIGO, cantidadDinero: _transaccion.TRA_VALOR ?? 0);
                }
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Transaccion Creada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarTrasaccionesAsync()
        {
            try
            {
                var _transacciones = await myDbContext.TRANSACCIONES.Where(x => x.TRA_ESTADO == true).ToListAsync();
                if (_transacciones == null || !_transacciones.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay transacciones.");

                return ResponseClass.Response(statusCode: 200, data: _transacciones);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarTrasaccionesPorFechaAsync(FiltrosDTO filtro)
        {

            try
            {
                var _transacciones = await myDbContext.TRANSACCIONES
                    .Where(x => x.TRA_FECHACREACION >= filtro.FechaInicio.ToLocalTime() && x.TRA_FECHACREACION <= filtro.FechaFin.ToLocalTime() && x.TRA_ESTADO == true &&
                    x.TRA_TIPO != TiposTransacciones.COSTOENVIO.Nombre)
                    .Select(x => new Transacciones
                    {
                        TRA_CODIGO = x.TRA_CODIGO,
                        TIC_CUENTA = x.TIC_CUENTA,
                        TRA_TIPO = x.TRA_TIPO,
                        TRA_FECHACREACION = x.TRA_FECHACREACION,
                        TRA_CONFIRMADA = x.TRA_CONFIRMADA,
                        TRA_ESTADO = x.TRA_ESTADO,
                        TRA_FECHACONFIRMACION = x.TRA_FECHACONFIRMACION,
                        TRA_CODIGOENLACE = x.TRA_CODIGOENLACE,
                        TRA_FUEANULADA = x.TRA_FUEANULADA,
                        TRA_NUMEROTRANSACCIONBANCO = x.TRA_NUMEROTRANSACCIONBANCO,
                        USU_CEDULA_CONFIRMADOR = x.USU_CEDULA_CONFIRMADOR,
                        TRA_VALOR = x.TRA_VALOR,
                        TIC_CODIGO = x.TIC_CODIGO,
                        TIC_NOMBRE = x.TIPOSCUENTAS.TIC_NOMBRE,
                        USU_NOMBRE = x.USU_CEDULA_CONFIRMADOR == null ? "" : myDbContext.USUARIOS.Where(u => u.USU_CEDULA == x.USU_CEDULA_CONFIRMADOR).Select(u => u.USU_NOMBRE).FirstOrDefault(),
                        GAS_VALOR = x.TRA_TIPO == TiposTransacciones.VENTA.Nombre ? myDbContext.GASTOS.Where(u => u.VEN_CODIGO == Convert.ToInt64(x.TRA_CODIGOENLACE)).Select(u => u.GAS_VALOR).FirstOrDefault() : 0,
                        CLI_NOMBRE = x.TRA_TIPO == TiposTransacciones.VENTA.Nombre ? myDbContext.VENTAS.Where(u => u.VEN_CODIGO == Convert.ToInt32(x.TRA_CODIGOENLACE)).Select(u => u.CLI_NOMBRE).FirstOrDefault() : "",
                    })
                    .OrderByDescending(x => x.TRA_CODIGO)
                    .ToListAsync();
                if (_transacciones == null || !_transacciones.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay transacciones.");

                return ResponseClass.Response(statusCode: 200, data: _transacciones);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarTrasaccionesPorFechaYCuentaAsync(int cuenta, FiltrosDTO filtro)
        {

            try
            {
                var _transacciones = await myDbContext.TRANSACCIONES
                    .Where(x => x.TIC_CODIGO == cuenta && x.TRA_FECHACREACION >= filtro.FechaInicio.ToLocalTime() && x.TRA_FECHACREACION <= filtro.FechaFin.ToLocalTime() && x.TRA_ESTADO == true).
                    Select(x => new Transacciones
                    {
                        TRA_CODIGO = x.TRA_CODIGO,
                        TIC_CUENTA = x.TIC_CUENTA,
                        TRA_TIPO = x.TRA_TIPO,
                        TRA_FECHACREACION = x.TRA_FECHACREACION,
                        TRA_CONFIRMADA = x.TRA_CONFIRMADA,
                        TRA_ESTADO = x.TRA_ESTADO,
                        TRA_FECHACONFIRMACION = x.TRA_FECHACONFIRMACION,
                        TRA_CODIGOENLACE = x.TRA_CODIGOENLACE,
                        TRA_FUEANULADA = x.TRA_FUEANULADA,
                        TRA_NUMEROTRANSACCIONBANCO = x.TRA_NUMEROTRANSACCIONBANCO,
                        USU_CEDULA_CONFIRMADOR = x.USU_CEDULA_CONFIRMADOR,
                        TRA_VALOR = x.TRA_VALOR,
                        TIC_CODIGO = x.TIC_CODIGO,
                        TIC_NOMBRE = x.TIPOSCUENTAS.TIC_NOMBRE,
                        USU_NOMBRE = x.USU_CEDULA_CONFIRMADOR == null ? myDbContext.USUARIOS.Where(u => u.USU_CEDULA == x.USU_CEDULA_CONFIRMADOR).Select(u => u.USU_NOMBRE).FirstOrDefault() : x.USU_CEDULA_CONFIRMADOR,
                        GAS_VALOR = x.TRA_TIPO == TiposTransacciones.VENTA.Nombre ? myDbContext.GASTOS.Where(u => u.VEN_CODIGO == Convert.ToInt64(x.TRA_CODIGOENLACE)).Select(u => u.GAS_VALOR).FirstOrDefault() : 0,
                        CLI_NOMBRE = x.TRA_TIPO == TiposTransacciones.VENTA.Nombre ? myDbContext.VENTAS.Where(u => u.VEN_CODIGO == Convert.ToInt32(x.TRA_CODIGOENLACE)).Select(u => u.CLI_NOMBRE).FirstOrDefault() : "",
                    })
                    .OrderByDescending(x => x.TRA_CODIGO)
                    .ToListAsync();
                if (_transacciones == null || !_transacciones.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay transacciones.");

                return ResponseClass.Response(statusCode: 200, data: _transacciones);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarTrasaccionPorIDAsync(int Id)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.Where(x => x.TRA_ESTADO == true && x.TRA_CODIGO == Id).FirstOrDefaultAsync();
                return _transaccion == null
                    ? throw new Exception($"La transaccion con el codigo {Id} no existe.")
                    : ResponseClass.Response(statusCode: 200, data: _transaccion);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarTrasaccionPorIDEnlaceAsync(string idEnlace)
        {
            try
            {
                var _transacciones = await myDbContext.TRANSACCIONES.Where(x => x.TRA_ESTADO == true && x.TRA_CODIGOENLACE == idEnlace).ToListAsync();
                return _transacciones == null
                    ? throw new Exception($"No hay Transacciones con el codigo de enlace {idEnlace}.")
                    : ResponseClass.Response(statusCode: 200, data: _transacciones);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> CrearTrasaccionEntreCuentasAsync(TransaccionEntreCuentas transaccion)
        {
            try
            {
                // transaccion de origen
                ArgumentNullException.ThrowIfNull(argument: transaccion.IdCuentaOrigen, nameof(transaccion.IdCuentaOrigen));
                TiposTransacciones tipoTransaccion = TiposTransacciones.TRANSFERENCIA;
                await CrearTrasaccionAsync(new()
                {
                    TIC_CUENTA = transaccion.IdCuentaOrigen ?? 0,
                    TIC_CODIGO = transaccion.IdCuentaOrigen ?? 0,
                    TRA_TIPO = tipoTransaccion.Nombre,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = true,
                    TRA_ESTADO = true,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = null,
                    TRA_FUEANULADA = false,
                    TRA_NUMEROTRANSACCIONBANCO = 0,
                    USU_CEDULA_CONFIRMADOR = transaccion.CedulaConfirmador,
                    TRA_VALOR = -transaccion.ValorTranferencia,
                });
                // transaccion de destino
                await CrearTrasaccionAsync(new()
                {
                    TIC_CUENTA = transaccion.IdCuentaDestino,
                    TIC_CODIGO = transaccion.IdCuentaDestino,
                    TRA_TIPO = tipoTransaccion.Nombre,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = true,
                    TRA_ESTADO = true,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = null,
                    TRA_FUEANULADA = false,
                    TRA_NUMEROTRANSACCIONBANCO = 0,
                    USU_CEDULA_CONFIRMADOR = transaccion.CedulaConfirmador,
                    TRA_VALOR = transaccion.ValorTranferencia,
                });
                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Realizada Exitosamente.");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> AjustarDineroAUnaCuenta(TransaccionEntreCuentas transaccion)
        {
            try
            {
                var _tipoCuenta = await myDbContext.TIPOSCUENTAS.FindAsync(transaccion.IdCuentaDestino);
                if (_tipoCuenta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La cuenta con el codigo {transaccion.IdCuentaDestino} no existe.");
                TiposTransacciones tipoTransaccion = TiposTransacciones.MOVIMIENTO;

                await CrearTrasaccionAsync(new()
                {
                    TIC_CUENTA = transaccion.IdCuentaDestino,
                    TIC_CODIGO = transaccion.IdCuentaDestino,
                    TRA_TIPO = tipoTransaccion.Nombre,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = true,
                    TRA_ESTADO = true,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = null,
                    TRA_FUEANULADA = false,
                    TRA_NUMEROTRANSACCIONBANCO = 0,
                    USU_CEDULA_CONFIRMADOR = transaccion.CedulaConfirmador,
                    TRA_VALOR = (transaccion.ValorTranferencia - _tipoCuenta.TIC_DINEROTOTAL),
                });
                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Realizada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region private Methods
        private bool ExisteTrasaccion(int id)
        {
            return myDbContext.TIPOSCUENTAS.Any(e => e.TIC_CODIGO == id);
        }
        private async Task AgregarDineroCuentas(int idCuenta, float cantidadDinero)
        {
            var cuenta = await myDbContext.TIPOSCUENTAS.Where(x => x.TIC_CODIGO == idCuenta).FirstOrDefaultAsync()
                ?? throw new Exception("No se puede agregar el dinero de la Transaccion. La Cuenta no Existe.");
            cuenta.TIC_DINEROTOTAL += (cantidadDinero);

            myDbContext.Entry(cuenta).State = EntityState.Modified;
            await myDbContext.SaveChangesAsync();
        }
        private async Task RetornarDineroCuentas(int idCuenta, float cantidadDinero)
        {
            var cuenta = await myDbContext.TIPOSCUENTAS.Where(x => x.TIC_CODIGO == idCuenta).FirstOrDefaultAsync()
                ?? throw new Exception("No se puede retornar el dinero de la Transaccion. La Cuenta no Existe.");
            cuenta.TIC_DINEROTOTAL -= (cantidadDinero);

            myDbContext.Entry(cuenta).State = EntityState.Modified;
            await myDbContext.SaveChangesAsync();
        }
        private async Task CentroDeConfirmacionDeEnlaces(TiposTransacciones tipoTransaccion, int idEnlace)
        {
            try
            {
                if (tipoTransaccion.Nombre == TiposTransacciones.VENTA.Nombre)
                {
                    await catalogoVentas.ActualizarEstadoVentaAsync(idEnlace);
                    return;
                }
                if (tipoTransaccion.Nombre == TiposTransacciones.GASTO.Nombre)
                {
                    await catalogoGastos.CerrarGasto(idEnlace);
                    return;
                }
                if (tipoTransaccion.Nombre == TiposTransacciones.COMPRA.Nombre)
                {
                    catalogoCompras.ConfirmarCompra(idEnlace);
                    return;
                }
                if (tipoTransaccion.Nombre == TiposTransacciones.ABONO.Nombre)
                {
                    await catalogoVentas.ConfirmarAbonoAsync(idEnlace);
                    return;
                }
                if (tipoTransaccion.Nombre == TiposTransacciones.TRANSFERENCIA.Nombre)
                {
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<(ResponseObject, TRANSACCIONES?)> ConfirmarTransacciones(int Id, string usurioConfirmador)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.FindAsync(Id) ?? throw new Exception($"La transaccion con el codigo {Id} no existe.");
                if (_transaccion.TRA_CONFIRMADA == true)
                    return (ResponseClass.Response(statusCode: 204, message: $"Transaccion ya confirmada Exitosamente."), _transaccion);

                _transaccion.TRA_CONFIRMADA = true;
                _transaccion.USU_CEDULA_CONFIRMADOR = usurioConfirmador;
                _transaccion.TRA_FECHACONFIRMACION = DateTime.Now;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                // cambiar estado del enlace
                await CentroDeConfirmacionDeEnlaces(tipoTransaccion: TiposTransacciones.GetTipoTransaccion(_transaccion.TRA_TIPO),
                    idEnlace: Convert.ToInt32(_transaccion.TRA_CODIGOENLACE));

                await AgregarDineroCuentas(idCuenta: _transaccion.TIC_CODIGO, cantidadDinero: _transaccion.TRA_VALOR ?? 0);
                await myDbContext.SaveChangesAsync();

                return (ResponseClass.Response(statusCode: 204, message: $"Transaccion confirmada Exitosamente."), _transaccion);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
