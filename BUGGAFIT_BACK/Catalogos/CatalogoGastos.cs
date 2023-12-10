using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoGastos : ICatalogoGastos
    {
        private readonly MyDBContext myDbContext;
        private readonly ICatalogoTransacciones? catalogoTransacciones;

        public CatalogoGastos(MyDBContext myDbContext, ICatalogoTransacciones catalogoTransacciones)
        {
            this.myDbContext = myDbContext;
            this.catalogoTransacciones = catalogoTransacciones;

        }

        public CatalogoGastos(MyDBContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<ResponseObject> ActualizarGastoAsync(Gasto gasto)
        {
            ArgumentNullException.ThrowIfNull(gasto.GAS_CODIGO, nameof(gasto));
            try
            {
                int ven_codigo = myDbContext.VENTAS.First().VEN_CODIGO;
                GASTOS _gasto = new()
                {
                    GAS_CODIGO = gasto.GAS_CODIGO,
                    GAS_FECHAGASTO = gasto.GAS_FECHAGASTO.ToLocalTime(),
                    MOG_CODIGO = gasto.MOG_CODIGO,
                    GAS_VALOR = gasto.GAS_VALOR,
                    TIC_CODIGO = gasto.TIC_CODIGO,
                    GAS_ESTADO = gasto.GAS_ESTADO,
                    USU_CEDULA = gasto.USU_CEDULA,
                    GAS_PENDIENTE = gasto.GAS_PENDIENTE,
                    GAS_OBSERVACIONES = gasto.GAS_OBSERVACIONES,
                    VEN_CODIGO = ven_codigo,
                };
                myDbContext.Entry(_gasto).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteGasto(gasto.GAS_CODIGO))
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {gasto.GAS_CODIGO} no existe.");
                throw;
            }
            catch (Exception) { throw; }
            return ResponseClass.Response(statusCode: 204, message: $"Gasto Actualizado Exitosamente.");
        }

        public async Task<ResponseObject> BorrarGastoAsync(int Id)
        {
            try
            {
                var _gasto = await myDbContext.GASTOS.FindAsync(Id);
                if (_gasto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {Id} no existe.");

                _gasto.GAS_ESTADO = false;
                myDbContext.Entry(_gasto).State = EntityState.Modified;
                await catalogoTransacciones.BorrarTrasaccionPorIdEnlaceAsync(_gasto.GAS_CODIGO.ToString());

                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Gasto Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> AnularGastoAsync(int id)
        {
            try
            {
                var _gasto = await myDbContext.GASTOS.FindAsync(id);
                if (_gasto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La transaccion con el codigo {id} no existe.");

                _gasto.GAS_ESANULADA = true;
                myDbContext.Entry(_gasto).State = EntityState.Modified;
                var _transaccion = await myDbContext.TRANSACCIONES
                    .Where(x => x.TRA_CODIGOENLACE == _gasto.GAS_CODIGO.ToString() && x.TRA_TIPO == TiposTransacciones.GASTO.Nombre)
                    .FirstOrDefaultAsync();

                if (_transaccion == null)
                {
                    return ResponseClass.Response(statusCode: 204, message: $"Venta Analudada Exitosamente.");
                }
                await catalogoTransacciones.AnularTrasaccionesAsync(_transaccion.TRA_CODIGO);

                await myDbContext.SaveChangesAsync();
                return ResponseClass.Response(statusCode: 204, message: $"Venta Analudada Exitosamente. {(_transaccion == null ? "No habian transacciones asociadas." : "")}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> RemoverGastoAsync(int id)
        {
            try
            {
                var _gasto = await myDbContext.GASTOS.FindAsync(id);
                if (_gasto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {id} no existe.");

                myDbContext.GASTOS.Remove(_gasto);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Gasto Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ResponseObject> CrearGastoAsync(Gasto gasto)
        {
            try
            {
                int ven_codigo = myDbContext.VENTAS.First().VEN_CODIGO;
                bool pendiente = true;
                if (myDbContext.TIPOSCUENTAS.Where(x => x.TIC_CODIGO == gasto.TIC_CODIGO).FirstOrDefault().TIC_NOMBRE.ToLower().Contains("efectivo") == true)
                {
                    pendiente = false;
                }
                GASTOS _gasto = new()
                {
                    GAS_CODIGO = gasto.GAS_CODIGO,
                    GAS_FECHACREACION = DateTime.Now,
                    GAS_FECHAGASTO = gasto.GAS_FECHAGASTO.ToLocalTime(),
                    MOG_CODIGO = gasto.MOG_CODIGO,
                    GAS_VALOR = gasto.GAS_VALOR,
                    TIC_CODIGO = gasto.TIC_CODIGO,
                    GAS_ESTADO = true,
                    USU_CEDULA = gasto.USU_CEDULA,
                    GAS_PENDIENTE = pendiente,
                    GAS_OBSERVACIONES = gasto.GAS_OBSERVACIONES,
                    VEN_CODIGO = ven_codigo,
                };
                myDbContext.GASTOS.Add(_gasto);
                await myDbContext.SaveChangesAsync();

                if (!_gasto.TipoCuentas.TIC_NOMBRE.ToLower().Contains("credito"))
                {
                    var tipoTransaccion = TiposTransacciones.GASTO;
                    await catalogoTransacciones.CrearTrasaccionAsync(new()
                    {
                        TIC_CUENTA = _gasto.TIC_CODIGO,
                        TIC_CODIGO = _gasto.TIC_CODIGO,
                        TRA_TIPO = tipoTransaccion.Nombre,
                        TRA_FECHACREACION = DateTime.Now,
                        TRA_CONFIRMADA = false,
                        TRA_ESTADO = true,
                        TRA_FECHACONFIRMACION = null,
                        TRA_CODIGOENLACE = _gasto.GAS_CODIGO.ToString(),
                        TRA_FUEANULADA = false,
                        TRA_NUMEROTRANSACCIONBANCO = 0,
                        USU_CEDULA_CONFIRMADOR = null,
                        TRA_VALOR = tipoTransaccion.EsRetiroDeDinero ? -(_gasto.GAS_VALOR) : gasto.GAS_VALOR,
                    });
                }

                return ResponseClass.Response(statusCode: 201, message: $"Gasto Creado Exitosamente.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ResponseObject> CrearGastoVentaAsync(Gasto gasto)
        {
            try
            {
                int codigoMotivo = myDbContext.MOTIVOSGASTOS.Where(x => x.MOG_NOMBRE.ToUpper().Contains("ENVIO") && x.MOG_ESTADO == true).Select(x => x.MOG_CODIGO).FirstAsync().Result;
                float valor = myDbContext.VENTAS.Where(x => x.VEN_CODIGO == gasto.VEN_CODIGO).Select(x => x.TIPOSENVIOS.TIP_VALOR).FirstAsync().Result;
                bool pendiente = true;

                if (myDbContext.TIPOSCUENTAS.Where(x => x.TIC_CODIGO == gasto.TIC_CODIGO).FirstOrDefault().TIC_NOMBRE.ToLower().Contains("efectivo") == true)
                {
                    pendiente = false;
                }
                GASTOS _gasto = new()
                {
                    GAS_CODIGO = gasto.GAS_CODIGO,
                    GAS_FECHACREACION = DateTime.Now,
                    GAS_FECHAGASTO = DateTime.Now,
                    MOG_CODIGO = codigoMotivo,
                    GAS_VALOR = valor,
                    TIC_CODIGO = gasto.TIC_CODIGO,
                    GAS_ESTADO = gasto.GAS_ESTADO,
                    USU_CEDULA = gasto.USU_CEDULA,
                    GAS_PENDIENTE = pendiente,
                    GAS_OBSERVACIONES = gasto.GAS_OBSERVACIONES,
                    VEN_CODIGO = gasto.VEN_CODIGO,
                };
                myDbContext.GASTOS.Add(_gasto);
                await myDbContext.SaveChangesAsync();

                // creacion de las transacciones de envio
                if (!_gasto.TipoCuentas.TIC_NOMBRE.ToLower().Contains("credito"))
                {
                    var tipoTransaccion = TiposTransacciones.GASTO;
                    await catalogoTransacciones.CrearTrasaccionAsync(new()
                    {
                        TIC_CUENTA = _gasto.TIC_CODIGO,
                        TIC_CODIGO = _gasto.TIC_CODIGO,
                        TRA_TIPO = tipoTransaccion.Nombre,
                        TRA_FECHACREACION = DateTime.Now,
                        TRA_CONFIRMADA = true,
                        TRA_ESTADO = true,
                        TRA_FECHACONFIRMACION = DateTime.Now,
                        TRA_CODIGOENLACE = _gasto.GAS_CODIGO.ToString(),
                        TRA_FUEANULADA = false,
                        TRA_NUMEROTRANSACCIONBANCO = 0,
                        USU_CEDULA_CONFIRMADOR = _gasto.USU_CEDULA,
                        TRA_VALOR = tipoTransaccion.EsRetiroDeDinero ? -(_gasto.GAS_VALOR) : gasto.GAS_VALOR,
                    });
                }

                return ResponseClass.Response(statusCode: 201, message: $"Gasto Creado Exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseClass.Response(statusCode: 500, message: $"Error al crear el gasto");
            }
        }

        public async Task<ResponseObject> ListarGastoPorIDAsync(int Id)
        {
            try
            {
                var _producto = await myDbContext.GASTOS.FindAsync(Id);
                if (_producto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: _producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarGastosAsync()
        {
            try
            {
                var _gastos = await myDbContext.GASTOS.Where(x => x.GAS_ESTADO == true).ToListAsync();
                if (_gastos == null || !_gastos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay gastos.");

                return ResponseClass.Response(statusCode: 200, data: _gastos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarMotivoGastosDeEnvioAsync()
        {
            try
            {
                var _gastos = await myDbContext.MOTIVOSGASTOS.Where(x => x.MOG_NOMBRE.ToUpper().StartsWith("ENVIO") && x.MOG_ESTADO == true).ToListAsync();
                if (_gastos == null || !_gastos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay motivos de envio.");

                return ResponseClass.Response(statusCode: 200, data: _gastos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> CerrarGasto(int id)
        {
            try
            {
                var _gasto = await myDbContext.GASTOS.FindAsync(id);
                if (_gasto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {id} no existe.");

                _gasto.GAS_PENDIENTE = false;
                myDbContext.Entry(_gasto).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Gasto Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ExisteGasto(int id)
        {
            return myDbContext.GASTOS.Any(e => e.GAS_CODIGO == id);
        }

        public async Task<ResponseObject> ListarGastosPorFecha(FiltrosDTO filtro)
        {
            try
            {
                List<Gasto> gastos = new();
                // Accede a la instancia de MyDBContext a través de ConexionBD 

                // Realiza consultas de Entity Framework aquí
                gastos = await myDbContext.GASTOS.Where(x => x.GAS_FECHAGASTO >= filtro.FechaInicio.ToLocalTime() && x.GAS_FECHAGASTO <= filtro.FechaFin.ToLocalTime() && x.GAS_ESTADO == true)
                    .Select(x => new Gasto
                    {
                        GAS_CODIGO = x.GAS_CODIGO,
                        GAS_FECHACREACION = x.GAS_FECHACREACION,
                        GAS_FECHAGASTO = x.GAS_FECHAGASTO,
                        MOG_CODIGO = x.MOG_CODIGO,
                        GAS_VALOR = x.GAS_VALOR,
                        TIC_CODIGO = x.TIC_CODIGO,
                        GAS_ESTADO = x.GAS_ESTADO,
                        USU_CEDULA = x.USU_CEDULA,
                        GAS_OBSERVACIONES = x.GAS_OBSERVACIONES,
                        GAS_PENDIENTE = x.GAS_PENDIENTE,
                        VEN_CODIGO = x.VEN_CODIGO,
                        USU_NOMBRE = x.Usuarios.USU_NOMBRE,
                        TIC_NOMBRE = x.TipoCuentas.TIC_NOMBRE,
                        MOG_NOMBRE = x.MOTIVOSGASTOS.MOG_NOMBRE,
                        GAS_ESANULADA = x.GAS_ESANULADA
                    })
                    .OrderByDescending(x => x.GAS_FECHAGASTO)
                    .ToListAsync();

                if (!gastos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay gastos.");
                return ResponseClass.Response(statusCode: 200, data: gastos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> EstadisticaGastos(FiltrosDTO filtro)
        {
            try
            {
                List<EstadisticasGastos> gastos = new();
                // Accede a la instancia de MyDBContext a través de ConexionBD 

                // Realiza consultas de Entity Framework aquí
                var _motivoGastos = await myDbContext.MOTIVOSGASTOS.Where(x => x.MOG_ESTADO == true).OrderBy(x => x.MOG_NOMBRE)
                    .Select(x => new EstadisticasGastos
                    {
                        MOG_CODIGO = x.MOG_CODIGO,
                        MOG_NOMBRE = x.MOG_NOMBRE,
                        MOG_VALORGASTADO = 0,
                    }).ToListAsync();

                if (!_motivoGastos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay motivos de gastos.");

                gastos = await myDbContext.GASTOS
                    .Where(x => x.GAS_FECHAGASTO >= filtro.FechaInicio.ToLocalTime()
                        && x.GAS_FECHAGASTO <= filtro.FechaFin.ToLocalTime()
                        && x.GAS_ESTADO == true
                        && x.GAS_PENDIENTE == false
                        && x.GAS_ESANULADA == false
                        && _motivoGastos.Select(y => y.MOG_CODIGO).Contains(x.MOG_CODIGO))
                    .GroupBy(x => x.MOG_CODIGO)
                    .Select(x => new EstadisticasGastos
                    {
                        MOG_CODIGO = x.Key,
                        MOG_NOMBRE = x.First().MOTIVOSGASTOS.MOG_NOMBRE,
                        MOG_VALORGASTADO = x.Sum(y => y.GAS_VALOR),
                    })
                    .OrderByDescending(x => x.MOG_CODIGO)
                    .ToListAsync();
                var result = gastos.Union(_motivoGastos, new EstadisticasGastosComparer()).ToList();

                if (!result.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay gastos.");
                return ResponseClass.Response(statusCode: 200, data: result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
