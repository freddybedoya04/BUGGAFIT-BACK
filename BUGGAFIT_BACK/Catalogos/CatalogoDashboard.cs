
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoDashboard : ICatalogoDashboard
    {
        private readonly MyDBContext myDbContext;

        public CatalogoDashboard(MyDBContext context)
        {
            myDbContext = context;
        }
        public async Task<ResponseObject> GetDashboard(FiltrosDTO filtros)
        {
            if (filtros.FechaInicio > filtros.FechaFin)
                return ResponseClass.Response(statusCode: 400, message: "La fecha fin no puede ser menor a la inicial.");

            try
            {
                DashboardClass dashboard = new();
                #region Datos de las cards
                var consulta = await (from v in myDbContext.VENTAS
                                      join dv in myDbContext.DETALLEVENTAS on v.VEN_CODIGO equals dv.VEN_CODIGO
                                      join p in myDbContext.PRODUCTOS on dv.PRO_CODIGO equals p.PRO_CODIGO
                                      where v.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime() && v.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && v.VEN_ESTADO == true
                                           && v.VEN_ESTADOCREDITO == false
                                           && v.VEN_ESTADOVENTA == true
                                           && v.VEN_ESANULADA != true
                                      group new { v, dv, p } by new { v.VEN_CODIGO, v.VEN_PRECIOTOTAL } into grouped
                                      select new
                                      {
                                          CodigodeVenta = grouped.Key.VEN_CODIGO,
                                          ValorTotaldelaVenta = grouped.Key.VEN_PRECIOTOTAL,
                                          NumeroTotaleProductosComprados = grouped.Sum(x => x.dv.VED_UNIDADES),
                                          CostoTotaldelosProductosVendidos = grouped.Sum(x => x.dv.VED_UNIDADES * x.p.PRO_PRECIO_COMPRA)
                                      }).ToListAsync();
                // buscamos la info necesaria para las cards
                var queryGastos = await myDbContext.GASTOS
                    .Where(x => x.GAS_FECHAGASTO >= filtros.FechaInicio.ToLocalTime() && x.GAS_FECHAGASTO <= filtros.FechaFin.ToLocalTime()
                    && x.GAS_ESTADO == true
                    && x.GAS_ESANULADA != true
                    && x.GAS_PENDIENTE == false)
                    .GroupBy(x => x.GAS_CODIGO)
                    .Select(x => new
                    {
                        gastosTotales = x.Sum(x => x.GAS_VALOR),
                        gastosNoPagos = x.Where(x => x.GAS_PENDIENTE == true).Sum(x => x.GAS_VALOR)
                    }).ToListAsync();
                var queryVentas = await myDbContext.VENTAS
                    .Where(x => x.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime() && x.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && x.VEN_ESTADO == true
                    && x.VEN_ESTADOCREDITO == false
                    && x.VEN_ESTADOVENTA == true
                    && x.VEN_ESANULADA != true)
                    .GroupBy(x => x.VEN_CODIGO)
                    .Select(x => new
                    {
                        ventasTotales = x.Sum(x => x.VEN_PRECIOTOTAL),
                        ventasACredito = x.Where(x => x.VEN_ESTADOCREDITO == true).Sum(x => x.VEN_PRECIOTOTAL)
                    }).ToListAsync();

                var queryCompras = await myDbContext.COMPRAS
                    .Where(x => x.COM_FECHACOMPRA >= filtros.FechaInicio.ToLocalTime() && x.COM_FECHACOMPRA <= filtros.FechaFin.ToLocalTime() && x.COM_ESTADO == true
                    && x.COM_ESANULADA != true
                    && x.COM_CREDITO == false)
                    .GroupBy(x => x.COM_CODIGO)
                    .Select(x => new
                    {
                        comprasTotales = x.Sum(x => x.COM_VALORCOMPRA),
                        comprasNoPagas = x.Where(x => x.COM_CREDITO == true).Sum(x => x.COM_VALORCOMPRA)
                    }).ToListAsync();

                var ventascredito = myDbContext.VENTAS
                    .Where(x => x.CLIENTES.CLI_ESCREDITO == true && x.VEN_ESTADO == true && x.VEN_ESTADOCREDITO == true && x.VEN_ESANULADA != true)
                    .Sum(x => x.VEN_PRECIOTOTAL);

                var abonos = myDbContext.CARTERAS
                    .Where(x => x.VENTA.CLIENTES.CLI_ESCREDITO == true && x.CAR_ESTADO == true && x.CAR_ESTADOCREDITO == 1 && x.CAR_ESANULADA != true)
                    .Sum(x => x.CAR_VALORABONADO);
                //calculamos variables adicionales 
                double deudasGastos, deudasCompras = 0;
                deudasGastos = queryGastos.Sum(x => x.gastosNoPagos);
                deudasCompras = queryCompras.Sum(x => x.comprasNoPagas);

                // guardamos los datos de las cards en el objeto 
                dashboard.DatosCards.SumaCreditos = ventascredito - abonos;
                dashboard.DatosCards.SumaCompras = queryCompras.Sum(x => x.comprasTotales);
                dashboard.DatosCards.SumaDeudas = deudasCompras + deudasGastos;
                dashboard.DatosCards.SumaGastos = queryGastos.Sum(x => x.gastosTotales);
                dashboard.DatosCards.SumaVentas = queryVentas.Sum(x => x.ventasTotales);
                dashboard.DatosCards.Utilidades = dashboard.DatosCards.SumaVentas - dashboard.DatosCards.SumaGastos - consulta.Sum(x => x.CostoTotaldelosProductosVendidos);
                dashboard.DatosCards.UtilidadesBrutas = dashboard.DatosCards.SumaVentas - dashboard.DatosCards.SumaGastos - dashboard.DatosCards.SumaCompras;
                #endregion

                #region datos de las graficas
                dashboard.DatosGraficas.VentasRealizadas = await myDbContext.VENTAS
                    .Where(x => x.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime() && x.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && x.VEN_ESTADO == true && x.VEN_ESANULADA != true)
                    .Select(x => new Ventas
                    {
                        VEN_CODIGO = x.VEN_CODIGO,
                        VEN_FECHAVENTA = x.VEN_FECHAVENTA,
                        VEN_TIPOPAGO = x.VEN_TIPOPAGO,
                        TIC_CODIGO = x.TIC_CODIGO,
                        CLI_ID = x.CLI_ID,
                        VEN_PRECIOTOTAL = x.VEN_PRECIOTOTAL,
                        VEN_ESTADOCREDITO = x.VEN_ESTADOCREDITO ?? false,
                        VEN_ENVIO = x.VEN_ENVIO ?? false,
                        VEN_DOMICILIO = x.VEN_DOMICILIO ?? false,
                        VEN_OBSERVACIONES = x.VEN_OBSERVACIONES,
                        VEN_ACTUALIZACION = x.VEN_ACTUALIZACION ?? new DateTime(),
                        USU_CEDULA = x.USU_CEDULA,
                        VEN_ESTADOVENTA = x.VEN_ESTADOVENTA,
                        VEN_ESTADO = x.VEN_ESTADO,
                    }).Take(10).ToListAsync();
                dashboard.DatosGraficas.ProductosVendidos = await (from ven in myDbContext.VENTAS
                                                                   join det in myDbContext.DETALLEVENTAS
                                                                   on ven.VEN_CODIGO equals det.VEN_CODIGO
                                                                   join pro in myDbContext.PRODUCTOS
                                                                   on det.PRO_CODIGO equals pro.PRO_CODIGO
                                                                   where ven.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime()
                                                                   && ven.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && ven.VEN_ESTADO == true && ven.VEN_ESANULADA != true
                                                                   group det by new { pro.PRO_CODIGO, pro.PRO_NOMBRE, det.VED_PRECIOVENTA_UND } into g
                                                                   select new ListaProductosVendidos
                                                                   {
                                                                       Codigo = g.Key.PRO_CODIGO,
                                                                       Nombre = g.Key.PRO_NOMBRE,
                                                                       Precio = g.Key.VED_PRECIOVENTA_UND,
                                                                       CantidadProducto = g.Sum(x => x.VED_UNIDADES),
                                                                   }).OrderByDescending(g => g.CantidadProducto).ToListAsync();



                dashboard.DatosGraficas.IngresosCuentas = await (from ven in myDbContext.VENTAS
                                                                 join cu in myDbContext.TIPOSCUENTAS
                                                                 on ven.TIC_CODIGO equals cu.TIC_CODIGO
                                                                 where ven.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime()
                                                                 && ven.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && ven.VEN_ESTADO == true
                                                                 && ven.VEN_ESTADOVENTA == true && ven.VEN_ESANULADA != true
                                                                 group ven by ven.TIC_CODIGO into g
                                                                 select new MovimientoCuentas
                                                                 {
                                                                     Codigo = g.Key,
                                                                     Nombre = g.First().TIPOSCUENTAS.TIC_NOMBRE,
                                                                     MovimientoTotal = g.Sum(x => x.VEN_PRECIOTOTAL),

                                                                 }).ToListAsync();
                dashboard.DatosGraficas.AbonosCuentas = await (from car in myDbContext.CARTERAS
                                                               join cu in myDbContext.TIPOSCUENTAS
                                                               on car.TIC_CODIGO equals cu.TIC_CODIGO
                                                               where car.CAR_FECHACREDITO >= filtros.FechaInicio.ToLocalTime()
                                                               && car.CAR_FECHACREDITO <= filtros.FechaFin.ToLocalTime() && car.CAR_ESTADO == true
                                                               && car.CAR_ESANULADA != true && car.CAR_ESTADOCREDITO == 1
                                                               group car by car.TIC_CODIGO into g
                                                               select new MovimientoCuentas
                                                               {
                                                                   Codigo = g.Key,
                                                                   Nombre = g.First().TIPOSCUENTAS.TIC_NOMBRE,
                                                                   MovimientoTotal = g.Sum(x => x.CAR_VALORABONADO),

                                                               }).ToListAsync();

                dashboard.DatosGraficas.GastosCuentas = await (from gas in myDbContext.GASTOS
                                                               where gas.GAS_FECHAGASTO >= filtros.FechaInicio.ToLocalTime()
                                                                 && gas.GAS_FECHAGASTO <= filtros.FechaFin.ToLocalTime() && gas.GAS_ESTADO == true
                                                                 && gas.GAS_PENDIENTE == false && gas.GAS_ESANULADA != true
                                                               group gas by gas.TipoCuentas.TIC_CODIGO into g
                                                               select new MovimientoCuentas
                                                               {
                                                                   Codigo = g.Key,
                                                                   Nombre = g.First().TipoCuentas.TIC_NOMBRE,
                                                                   MovimientoTotal = g.Sum(x => x.GAS_VALOR),

                                                               }).ToListAsync();
                dashboard.DatosGraficas.ComprasCuentas = await (from co in myDbContext.COMPRAS
                                                                where co.COM_FECHACOMPRA >= filtros.FechaInicio.ToLocalTime()
                                                                  && co.COM_FECHACOMPRA <= filtros.FechaFin.ToLocalTime() && co.COM_ESTADO == true
                                                                  && co.COM_CREDITO == false && co.COM_ESANULADA != true
                                                                group co by co.TipoCuenta.TIC_CODIGO into g
                                                                select new MovimientoCuentas
                                                                {
                                                                    Codigo = g.Key,
                                                                    Nombre = g.First().TipoCuenta.TIC_NOMBRE,
                                                                    MovimientoTotal = g.Sum(x => x.COM_VALORCOMPRA),

                                                                }).ToListAsync();

                dashboard.DatosGraficas.GastosCuentas = await (from co in myDbContext.GASTOS
                                                               where co.GAS_FECHAGASTO >= filtros.FechaInicio.ToLocalTime()
                                                                 && co.GAS_FECHAGASTO <= filtros.FechaFin.ToLocalTime() && co.GAS_ESTADO == true
                                                                 && co.GAS_PENDIENTE == false && co.GAS_ESANULADA != true
                                                               group co by co.TipoCuentas.TIC_CODIGO into g
                                                               select new MovimientoCuentas
                                                               {
                                                                   Codigo = g.Key,
                                                                   Nombre = g.First().TipoCuentas.TIC_NOMBRE,
                                                                   MovimientoTotal = g.Sum(x => x.GAS_VALOR),

                                                               }).ToListAsync();
                #endregion
                return ResponseClass.Response(statusCode: 200, data: dashboard, message: "OK.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
