﻿using BUGGAFIT_BACK.Clases;
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
                // buscamos la info necesaria para las cards
                var queryGastos = await myDbContext.GASTOS
                    .Where(x => x.GAS_FECHAGASTO >= filtros.FechaInicio.ToLocalTime() && x.GAS_FECHAGASTO <= filtros.FechaFin.ToLocalTime() && x.GAS_ESTADO == true)
                    .GroupBy(x => x.GAS_CODIGO)
                    .Select(x => new
                    {
                        gastosTotales = x.Sum(x => x.GAS_VALOR),
                        gastosNoPagos = x.Where(x => x.GAS_PENDIENTE == true).Sum(x => x.GAS_VALOR)
                    }).ToListAsync();
                var queryVentas = await myDbContext.VENTAS
                    .Where(x => x.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime() && x.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && x.VEN_ESTADO == true)
                    .GroupBy(x => x.VEN_CODIGO)
                    .Select(x => new
                    {
                        ventasTotales = x.Sum(x => x.VEN_PRECIOTOTAL),
                        ventasACredito = x.Where(x => x.VEN_ESTADOCREDITO == true).Sum(x => x.VEN_PRECIOTOTAL)
                    }).ToListAsync();

                var queryCompras = await myDbContext.COMPRAS
                    .Where(x => x.COM_FECHACOMPRA >= filtros.FechaInicio.ToLocalTime() && x.COM_FECHACOMPRA <= filtros.FechaFin.ToLocalTime() && x.COM_ESTADO == true)
                    .GroupBy(x => x.COM_CODIGO)
                    .Select(x => new
                    {
                        comprasTotales = x.Sum(x => x.COM_VALORCOMPRA),
                        comprasNoPagas = x.Where(x => x.COM_CREDITO == true).Sum(x => x.COM_VALORCOMPRA)
                    }).ToListAsync();
                // guardamos los datos de las cards en el objeto 
                dashboard.DatosCards.SumaCompras = queryCompras.Sum(x => x.comprasTotales);
                dashboard.DatosCards.SumaCreditos = queryVentas.Sum(x => x.ventasACredito);

                double deudasGastos, deudasCompras = 0;
                deudasGastos = queryGastos.Sum(x => x.gastosNoPagos);
                deudasCompras = queryCompras.Sum(x => x.comprasNoPagas);
                dashboard.DatosCards.SumaDeudas = deudasCompras + deudasGastos;

                dashboard.DatosCards.SumaGastos = queryGastos.Sum(x => x.gastosTotales);
                dashboard.DatosCards.SumaVentas = queryVentas.Sum(x => x.ventasTotales);
                #endregion

                #region datos de las graficas
                dashboard.DatosGraficas.VentasRealizadas = await myDbContext.VENTAS
                    .Where(x => x.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime() && x.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && x.VEN_ESTADO == true)
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
                dashboard.DatosGraficas.ProductosMasVendidos = await (from ven in myDbContext.VENTAS
                                                                      join det in myDbContext.DETALLEVENTAS
                                                                      on ven.VEN_CODIGO equals det.VEN_CODIGO
                                                                      join pro in myDbContext.PRODUCTOS
                                                                      on det.PRO_CODIGO equals pro.PRO_CODIGO
                                                                      group det by det.PRO_CODIGO into g
                                                                      select new ListaProductosVendidos
                                                                      {
                                                                          Codigo = g.Key,
                                                                          Nombre = g.First().PRODUCTOS.PRO_NOMBRE,
                                                                          CantidadProducto = g.Count(),

                                                                      }).ToListAsync();
                dashboard.DatosGraficas.IngresosCuentas = await (from ven in myDbContext.VENTAS
                                                                 join cu in myDbContext.TIPOSCUENTAS
                                                                 on ven.TIC_CODIGO equals cu.TIC_CODIGO
                                                                 group ven by ven.TIC_CODIGO into g
                                                                 select new IngresosCuentas
                                                                 {
                                                                     Codigo = g.Key,
                                                                     Nombre = g.First().TIPOSCUENTAS.TIC_NOMBRE,
                                                                     IngresosTotales = g.Sum(x => x.VEN_PRECIOTOTAL),

                                                                 }).ToListAsync();
                #endregion
                return ResponseClass.Response(statusCode: 200, data: dashboard, message: "OK.");
            }
            catch (Exception)
            {
                throw;
            }



            //.Where(x => x.VEN_FECHAVENTA >= filtros.FechaInicio.ToLocalTime() && x.VEN_FECHAVENTA <= filtros.FechaFin.ToLocalTime() && x.VEN_ESTADO == true)
            //.Join(
            //myDbContext.DETALLEVENTAS, 
            //detalle => detalle.VEN_CODIGO, 
            //venta => venta.VEN_CODIGO, 
            //(detalle,venta) => new ListaProductosVendidos
            //{

            //}).ToListAsync();
        }
    }
}