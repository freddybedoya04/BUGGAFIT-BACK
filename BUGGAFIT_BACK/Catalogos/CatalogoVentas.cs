using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoVentas : ICatalogoVentas
    {
        private readonly MyDBContext myDbContext;

        public CatalogoVentas(MyDBContext context)
        {
            myDbContext = context;
        }

        public async Task<ResponseObject> ActualizarVentaAsync(Ventas venta)
        {
            try
            {
                VENTAS _ventas = new()
                {
                    VEN_CODIGO = venta.VEN_CODIGO,
                    VEN_FECHAVENTA = venta.VEN_FECHAVENTA,
                    VEN_TIPOPAGO = venta.VEN_TIPOPAGO,
                    TIC_CODIGO = venta.TIC_CODIGO,
                    CLI_ID = venta.CLI_ID,
                    VEN_PRECIOTOTAL = venta.VEN_PRECIOTOTAL,
                    VEN_ESTADOCREDITO = venta.VEN_ESTADOCREDITO,
                    VEN_ENVIO = venta.VEN_ENVIO,
                    VEN_DOMICILIO = venta.VEN_DOMICILIO,
                    VEN_OBSERVACIONES = venta.VEN_OBSERVACIONES,
                    VEN_ACTUALIZACION = venta.VEN_ACTUALIZACION,
                    USU_CEDULA = venta.USU_CEDULA,
                    VEN_ESTADOVENTA = venta.VEN_ESTADOVENTA,
                    VEN_ESTADO = venta.VEN_ESTADO,
                };
                myDbContext.Entry(_ventas).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentasExists(venta.VEN_CODIGO))
                    return ResponseClass.Response(statusCode: 400, message: $"La venta con el codigo {venta.VEN_CODIGO} no existe.");
                throw;
            }
            catch (Exception) { throw; }

            return ResponseClass.Response(statusCode: 204, message: $"Venta Actualizada Exitosamente.");
        }
        public async Task<ResponseObject> ActualizarEstadoVentaAsync(int id)
        {
            try
            {
                VENTAS venta = myDbContext.VENTAS.Where(x => x.VEN_CODIGO == id).FirstOrDefault();
                venta.VEN_ESTADOVENTA = true;
                venta.VEN_ACTUALIZACION = DateTime.Now;
                myDbContext.Entry(venta).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return ResponseClass.Response(statusCode: 400, message: $"Error al actualizar el estado de la venta");
                throw;
            }

            return ResponseClass.Response(statusCode: 204, message: $"Venta Actualizada Exitosamente.");
        }
        public async Task<ResponseObject> BorrarVentaAsync(int Id)
        {
            try
            {
                var venta = await myDbContext.VENTAS.FindAsync(Id);
                if (venta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La venta con el codigo {Id} no existe.");

                venta.VEN_ESTADO = false;
                myDbContext.Entry(venta).State = EntityState.Modified;
                await RegresarProductosAlInventario(venta.VEN_CODIGO);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Venta Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> RemoverVentaAsync(int Id)
        {
            try
            {
                var venta = await myDbContext.VENTAS.FindAsync(Id);
                if (venta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La venta con el codigo {Id} no existe.");

                myDbContext.VENTAS.Remove(venta);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Venta Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ResponseObject> CrearDetalleVentaAsync(List<DetalleVenta> detalleVentas)
        {
            try
            {
                foreach (DetalleVenta detalle in detalleVentas)
                {
                    DETALLEVENTAS _detalleVentas = new()
                    {
                        VED_CODIGO = detalle.VED_CODIGO,
                        VEN_CODIGO = detalle.VEN_CODIGO,
                        PRO_CODIGO = detalle.PRO_CODIGO ?? "",
                        VED_UNIDADES = detalle.VED_UNIDADES,
                        VED_PRECIOVENTA_UND = detalle.VED_PRECIOVENTA_UND,
                        VED_VALORDESCUENTO_UND = detalle.VED_VALORDESCUENTO_UND,
                        VED_PRECIOVENTA_TOTAL = detalle.VED_PRECIOVENTA_TOTAL,
                        VED_ACTUALIZACION = DateTime.Now,
                        VED_ESTADO = true,
                    };
                    myDbContext.DETALLEVENTAS.Add(_detalleVentas);
                }
                await myDbContext.SaveChangesAsync();
                return ResponseClass.Response(statusCode: 201, message: $"Detalles de venta Creados Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearVentaAsync(Ventas venta)
        {
            try
            {
                VENTAS _ventas = new()
                {
                    VEN_CODIGO = venta.VEN_CODIGO,
                    VEN_FECHACREACION = DateTime.Now,
                    VEN_FECHAVENTA = venta.VEN_FECHAVENTA.ToLocalTime(),
                    VEN_TIPOPAGO = venta.VEN_TIPOPAGO,
                    TIC_CODIGO = venta.TIC_CODIGO,
                    CLI_ID = venta.CLI_ID,
                    VEN_PRECIOTOTAL = venta.VEN_PRECIOTOTAL,
                    VEN_ESTADOCREDITO = venta.VEN_ESTADOCREDITO,
                    VEN_ENVIO = venta.VEN_ENVIO,
                    VEN_DOMICILIO = venta.VEN_DOMICILIO,
                    VEN_OBSERVACIONES = venta.VEN_OBSERVACIONES,
                    VEN_ACTUALIZACION = DateTime.Now,
                    USU_CEDULA = venta.USU_CEDULA,
                    VEN_ESTADOVENTA = venta.VEN_ESTADOVENTA,
                    VEN_ESTADO = venta.VEN_ESTADO,
                };
                myDbContext.VENTAS.Add(_ventas);
                await myDbContext.SaveChangesAsync();
                if (venta.DetalleVentas != null)
                {
                    venta.DetalleVentas.ToList().ForEach(x => x.VEN_CODIGO = _ventas.VEN_CODIGO);

                    //creamos las tareas necesarias para la creacion de la venta
                    var tskCrearDetalleVentaAsync = Task.Run(() =>
                        CrearDetalleVentaAsync((List<DetalleVenta>)venta.DetalleVentas)
                    );
                    var tskRetirarProductosDelInventario = Task.Run(() =>
                        RetirarProductosDelInventario(_ventas.VEN_CODIGO)
                    );
                    List<Task> listTasks = new() { tskCrearDetalleVentaAsync, tskRetirarProductosDelInventario };

                    if (_ventas.VEN_ESTADOVENTA == true)
                    {
                        var tskCrearEntradaDeCartera = Task.Run(() =>
                            CrearEntradaDeCartera(_ventas.VEN_CODIGO, venta)
                        );
                        listTasks.Add(tskCrearEntradaDeCartera);
                    }
                    await Task.WhenAll(listTasks);
                }
                return ResponseClass.Response(statusCode: 201, data: _ventas.VEN_CODIGO, message: $"Venta Creada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarVentaPorIDAsync(int Id)
        {
            try
            {
                var ventas = await myDbContext.VENTAS.FindAsync(Id);
                if (ventas == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La venta con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: ventas);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarVentasAsync()
        {
            try
            {
                var ventas = await myDbContext.VENTAS.ToListAsync();
                if (ventas == null || !ventas.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay ventas");

                return ResponseClass.Response(statusCode: 200, data: ventas);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ListarVentasPorFechaAsync(FiltrosDTO filtro)
        {
            try
            {
               List<Ventas> ventas = await myDbContext.VENTAS
                    .Where(x => x.VEN_FECHAVENTA >= filtro.FechaInicio.ToLocalTime()
                                && x.VEN_FECHAVENTA <= filtro.FechaFin.ToLocalTime()
                                && x.VEN_ESTADO == true)
                    .Select(d => new Ventas
                        {
                            VEN_CODIGO = d.VEN_CODIGO,
                            VEN_FECHACREACION = d.VEN_FECHACREACION,
                            VEN_FECHAVENTA = d.VEN_FECHAVENTA,
                            VEN_TIPOPAGO = d.VEN_TIPOPAGO,
                            TIC_CODIGO = d.TIC_CODIGO,
                            CLI_ID = d.CLI_ID,
                            VEN_PRECIOTOTAL = d.VEN_PRECIOTOTAL,
                            VEN_ESTADOCREDITO = (bool)d.VEN_ESTADOCREDITO,
                            VEN_ENVIO = (bool)d.VEN_ENVIO,
                            VEN_DOMICILIO = (bool)d.VEN_DOMICILIO,
                            VEN_OBSERVACIONES = d.VEN_OBSERVACIONES,
                            VEN_ACTUALIZACION = (DateTime)d.VEN_ACTUALIZACION,
                            USU_CEDULA = d.USU_CEDULA,
                            VEN_ESTADOVENTA = d.VEN_ESTADOVENTA,
                            VEN_ESTADO = d.VEN_ESTADO,
                            CLI_NOMBRE = d.CLIENTES.CLI_NOMBRE,
                            CLI_DIRECCION = d.CLIENTES.CLI_DIRECCION,
                            CLI_TIPOCLIENTE = d.CLIENTES.CLI_TIPOCLIENTE,
                            CLI_UBICACION = d.CLIENTES.CLI_UBICACION,
                            TIC_NOMBRE = d.TIPOSCUENTAS.TIC_NOMBRE
                        }).OrderByDescending(x => x.VEN_FECHAVENTA).ToListAsync();

                return ResponseClass.Response(statusCode: 200, data: ventas);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VentasExists(int id)
        {
            return myDbContext.VENTAS.Any(e => e.VEN_CODIGO == id);
        }

        private async Task RegresarProductosAlInventario(int codigoVenta)
        {
            //obtenemos todos los detalle ventas relacionados a la venta
            var detallesDeVenta = await myDbContext.DETALLEVENTAS.Where(x => x.VEN_CODIGO == codigoVenta).ToListAsync();
            foreach (var detalle in detallesDeVenta)
            {
                var _producto = await myDbContext.PRODUCTOS.Where(x => x.PRO_CODIGO == detalle.PRO_CODIGO).FirstOrDefaultAsync();
                if (_producto == null)
                    continue;

                _producto.PRO_UNIDADES_DISPONIBLES += detalle.VED_UNIDADES; //devolvemos las unidades al inventario
                myDbContext.Entry(_producto).State = EntityState.Modified;
                detalle.VED_ESTADO = false;
                myDbContext.Entry(detalle).State = EntityState.Modified;

                await myDbContext.SaveChangesAsync();
            }
        }
        private async Task RetirarProductosDelInventario(int codigoVenta)
        {
            try
            {
                //obtenemos todos los detalle ventas relacionados a la venta
                var detallesDeVenta = await myDbContext.DETALLEVENTAS.Where(x => x.VEN_CODIGO == codigoVenta).ToListAsync();
                foreach (var detalle in detallesDeVenta)
                {
                    var _producto = await myDbContext.PRODUCTOS.Where(x => x.PRO_CODIGO == detalle.PRO_CODIGO).FirstOrDefaultAsync();
                    if (_producto == null)
                        continue;

                    _producto.PRO_UNIDADES_DISPONIBLES -= detalle.VED_UNIDADES; //Retiramos las unidades del inventario
                    myDbContext.Entry(_producto).State = EntityState.Modified;

                    await myDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task CrearEntradaDeCartera(int codigoVenta, Ventas venta)
        {
            try
            {
                CARTERAS _cartera = new()
                {
                    CAR_FECHACREACION = DateTime.Now,
                    CAR_FECHACREDITO = venta.VEN_FECHAVENTA.ToLocalTime(),
                    CAR_FECHAACTUALIZACION = DateTime.Now,
                    CAR_MOTIVO = "Credito de venta",
                    VEN_CODIGO = codigoVenta,
                    CAR_VALORCREDITO = venta.VEN_PRECIOTOTAL,
                    CAR_VALORABONADO = 0,
                    CAR_ESTADOCREDITO = 1,
                    CAR_ESTADO = true,
                };
                myDbContext.CARTERAS.Add(_cartera);

                await myDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarDetallePorCodigoVentaAsync(int id)
        {
            try
            {
                List<DetalleVenta>  Detalle =await myDbContext.DETALLEVENTAS.Where(d => d.VEN_CODIGO == id).Select(x => new DetalleVenta
                {
                    VED_CODIGO = x.VED_CODIGO,
                    VEN_CODIGO = x.VEN_CODIGO,
                    PRO_CODIGO = x.PRO_CODIGO,
                    PRO_NOMBRE = x.PRODUCTOS.PRO_NOMBRE,
                    VED_UNIDADES = x.VED_UNIDADES,
                    VED_PRECIOVENTA_UND = x.VED_PRECIOVENTA_UND,
                    VED_VALORDESCUENTO_UND = x.VED_VALORDESCUENTO_UND,
                    VED_PRECIOVENTA_TOTAL = x.VED_PRECIOVENTA_TOTAL,
                    VED_ACTUALIZACION = x.VED_ACTUALIZACION,
                    VED_ESTADO = x.VED_ESTADO
                }).ToListAsync();


                return ResponseClass.Response(statusCode: 200, data: Detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarAbonosPorCodigoVentaAsync(int id)
        {
            try
            {
                List<Cartera> carteras = await myDbContext.CARTERAS.Where(d => d.VEN_CODIGO == id).Select(x => new Cartera
                {
                    CAR_CODIGO = x.CAR_CODIGO,
                    VEN_CODIGO = x.VEN_CODIGO,
                    CAR_FECHACREDITO = x.CAR_FECHACREDITO,
                    CAR_VALORABONADO = x.CAR_VALORABONADO,
                    TIC_CODIGO = x.TIC_CODIGO,
                    TIC_NOMBRE = x.TIPOSCUENTAS.TIC_NOMBRE,
                }).OrderByDescending(x=>x.CAR_CODIGO).ToListAsync();


                return ResponseClass.Response(statusCode: 200, data: carteras);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
