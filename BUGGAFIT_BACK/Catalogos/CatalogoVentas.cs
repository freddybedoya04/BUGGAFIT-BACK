using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

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
                    VEN_FECHACREACION = venta.VEN_FECHACREACION,
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
                myDbContext.VENTAS.Add(_ventas);
                await myDbContext.SaveChangesAsync();
                if (venta.DetalleVentas != null)
                {
                    venta.DetalleVentas.ToList().ForEach(x => x.VEN_CODIGO = _ventas.VEN_CODIGO);
                    await CrearDetalleVentaAsync((List<DetalleVenta>)venta.DetalleVentas);
                    await RetirarProductosDelInventario(_ventas.VEN_CODIGO);
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

                await myDbContext.SaveChangesAsync();
            }
        }
        private async Task RetirarProductosDelInventario(int codigoVenta)
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
    }
}
