using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.ConexionBD; // Asegúrate de agregar esta línea
using BUGGAFIT_BACK.Modelos.Entidad;
using BUGGAFIT_BACK.Modelos;
using Microsoft.EntityFrameworkCore;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoCompras : ICatalogoCompras
    {
        private MyDBContext dbContext;
        private readonly ICatalogoTransacciones? catalogoTransacciones;


        public CatalogoCompras(MyDBContext Context, ICatalogoTransacciones catalogoTransacciones)
        {
            dbContext = Context;
            this.catalogoTransacciones = catalogoTransacciones;
        }

        public CatalogoCompras(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Compra> ListarComprasPorFecha(FiltrosDTO filtro)
        {
            try
            {
                // Accede a la instancia de MyDBContext a través de ConexionBD 
                using (var db = dbContext)
                {
                    // Realiza consultas de Entity Framework aquí
                    List<Compra> compras = db.COMPRAS.Where(x => x.COM_FECHACOMPRA >= filtro.FechaInicio.ToLocalTime() && x.COM_FECHACOMPRA <= filtro.FechaFin.ToLocalTime() && x.COM_ESTADO == true).Select(x => new Compra
                    {
                        COM_CODIGO = x.COM_CODIGO,
                        COM_FECHACREACION = x.COM_FECHACREACION,
                        COM_FECHACOMPRA = x.COM_FECHACOMPRA,
                        COM_VALORCOMPRA = x.COM_VALORCOMPRA,
                        COM_PROVEEDOR = x.COM_PROVEEDOR,
                        TIC_CODIGO = x.TIC_CODIGO,
                        TIC_NOMBRE = x.TipoCuenta.TIC_NOMBRE,
                        COM_FECHAACTUALIZACION = x.COM_FECHAACTUALIZACION,
                        COM_ENBODEGA = x.COM_ENBODEGA,
                        COM_ESTADO = x.COM_ESTADO,
                        COM_CREDITO = x.COM_CREDITO,
                        USU_CEDULA = x.USU_CEDULA,
                        COM_ESANULADA = x.COM_ESANULADA,
                        DetalleCompras = db.DETALLECOMPRAS.Where(d => d.COM_CODIGO == x.COM_CODIGO && d.DEC_ESTADO == true).Select(d => new DetalleCompra
                        {
                            COM_CODIGO = d.COM_CODIGO,
                            DEC_CODIGO = d.DEC_CODIGO,
                            DEC_UNIDADES = d.DEC_UNIDADES,
                            DEC_PRECIOCOMPRA_PRODUCTO = d.DEC_PRECIOCOMPRA_PRODUCTO,
                            DEC_PRECIOTOTAL = d.DEC_PRECIOTOTAL,
                            DEC_ESTADO = d.DEC_ESTADO,
                            PRO_CODIGO = d.PRO_CODIGO,
                            PRO_NOMBRE = d.PRODUCTO.PRO_NOMBRE,
                        }).ToList(),
                    }).OrderByDescending(x => x.COM_FECHACOMPRA).ToList();
                    return compras;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Compra BuscarCompraPorID(int id)
        {
            try
            {

                using (var db = dbContext)
                {
                    COMPRAS Compras = db.COMPRAS.Where(x => x.COM_CODIGO == id).FirstOrDefault();
                    Compra compra = new Compra();
                    if (compra != null)
                    {
                        compra.COM_CODIGO = Compras.COM_CODIGO;
                        compra.COM_FECHACREACION = Compras.COM_FECHACREACION;
                        compra.COM_FECHACOMPRA = Compras.COM_FECHACOMPRA;
                        compra.COM_VALORCOMPRA = Compras.COM_VALORCOMPRA;
                        compra.COM_PROVEEDOR = Compras.COM_PROVEEDOR;
                        compra.TIC_CODIGO = Compras.TIC_CODIGO;
                        compra.COM_FECHAACTUALIZACION = Compras.COM_FECHAACTUALIZACION;
                        compra.COM_ENBODEGA = Compras.COM_ENBODEGA;
                        compra.COM_ESTADO = Compras.COM_ESTADO;
                        compra.COM_CREDITO = Compras.COM_CREDITO;
                        compra.USU_CEDULA = Compras.USU_CEDULA;
                    }


                    return compra;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void CrearCompra(Compra nuevaCompra)
        {
            try
            {

                using (var db = dbContext)
                {
                    COMPRAS compras = new COMPRAS();
                    //bool pendiente = true;
                    //if (db.TIPOSCUENTAS.Where(x => x.TIC_CODIGO == nuevaCompra.TIC_CODIGO).FirstOrDefault().TIC_NOMBRE.ToLower().Contains("efectivo") == true)
                    //{
                    //    pendiente = false;
                    //}
                    //compras.COM_CODIGO = nuevaCompra.COM_CODIGO;
                    compras.COM_FECHACREACION = DateTime.Now;
                    compras.COM_FECHACOMPRA = nuevaCompra.COM_FECHACOMPRA.ToLocalTime();
                    compras.COM_VALORCOMPRA = nuevaCompra.COM_VALORCOMPRA;
                    compras.COM_PROVEEDOR = nuevaCompra.COM_PROVEEDOR;
                    compras.TIC_CODIGO = nuevaCompra.TIC_CODIGO;
                    compras.COM_FECHAACTUALIZACION = DateTime.Now;
                    compras.COM_ENBODEGA = nuevaCompra.COM_ENBODEGA;
                    compras.COM_ESTADO = true;
                    compras.COM_CREDITO = nuevaCompra.COM_CREDITO;
                    compras.USU_CEDULA = nuevaCompra.USU_CEDULA;

                    db.COMPRAS.Add(compras);
                    db.SaveChanges();

                    //crear detalles
                    
                    foreach (DetalleCompra item in nuevaCompra.DetalleCompras)
                    {
                        DETALLECOMPRAS Detalle = new DETALLECOMPRAS();
                        Detalle.COM_CODIGO = compras.COM_CODIGO;
                        Detalle.PRO_CODIGO = item.PRO_CODIGO;
                        Detalle.DEC_UNIDADES = item.DEC_UNIDADES;
                        Detalle.DEC_PRECIOCOMPRA_PRODUCTO = item.DEC_PRECIOCOMPRA_PRODUCTO;
                        Detalle.DEC_PRECIOTOTAL = item.DEC_PRECIOTOTAL;
                        Detalle.DEC_FECHACREACION = DateTime.Now;
                        Detalle.DEC_FECHAACTUALIZACION = DateTime.Now;
                        Detalle.DEC_ESTADO = true;

                        db.DETALLECOMPRAS.Add(Detalle);
                        //LOGICA PARA ACTUALIZAR PRODUCTOS SI ES QUE LA COMPRA ESTA EN BODEGA
                        if (nuevaCompra.COM_ENBODEGA == true)
                        {
                            PRODUCTOS producto = db.PRODUCTOS.Where(x => x.PRO_CODIGO == item.PRO_CODIGO).FirstOrDefault();
                            if (producto != null)
                            {
                                producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + item.DEC_UNIDADES;
                                producto.PRO_ACTUALIZACION = DateTime.Now;
                            }
                        }

                    }

                    var tipoTransaccion = TiposTransacciones.COMPRA;

                    catalogoTransacciones.CrearTrasaccionAsync(new()
                    {
                        TIC_CUENTA = compras.TIC_CODIGO,
                        TIC_CODIGO = compras.TIC_CODIGO,
                        TRA_TIPO = tipoTransaccion.Nombre,
                        TRA_FECHACREACION = DateTime.Now,
                        TRA_CONFIRMADA = false,
                        TRA_ESTADO = true,
                        TRA_FECHACONFIRMACION = null,
                        TRA_CODIGOENLACE = compras.COM_CODIGO.ToString(),
                        TRA_FUEANULADA = false,
                        TRA_NUMEROTRANSACCIONBANCO = 0,
                        USU_CEDULA_CONFIRMADOR = null,
                        TRA_VALOR = tipoTransaccion.EsRetiroDeDinero ? -(compras.COM_VALORCOMPRA) : compras.COM_VALORCOMPRA,
                    }).Wait();

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void ActualizarCompra(Compra nuevaCompra)
        {
            try
            {

                using (var db = dbContext)
                {
                    COMPRAS CompraAnterior = db.COMPRAS.Where(x => x.COM_CODIGO == nuevaCompra.COM_CODIGO).FirstOrDefault();
                    bool EstabaEnBodega = CompraAnterior.COM_ENBODEGA;
                    CompraAnterior.COM_FECHACOMPRA = nuevaCompra.COM_FECHACOMPRA.ToLocalTime();
                    CompraAnterior.COM_VALORCOMPRA = nuevaCompra.COM_VALORCOMPRA;
                    CompraAnterior.COM_PROVEEDOR = nuevaCompra.COM_PROVEEDOR;
                    CompraAnterior.TIC_CODIGO = nuevaCompra.TIC_CODIGO;
                    CompraAnterior.COM_FECHAACTUALIZACION = DateTime.Now;
                    CompraAnterior.COM_ENBODEGA = nuevaCompra.COM_ENBODEGA;
                    CompraAnterior.COM_ESTADO = nuevaCompra.COM_ESTADO;
                    CompraAnterior.COM_CREDITO = nuevaCompra.COM_CREDITO;
                    CompraAnterior.USU_CEDULA = nuevaCompra.USU_CEDULA;
                    db.SaveChanges();
                    //si estaba no estaba en bodega y va a pasar a estar en bodega debe ingresar la mercacia en el inventario
                    if(EstabaEnBodega==false && nuevaCompra.COM_ENBODEGA == true)
                    {
                        foreach (DetalleCompra item in nuevaCompra.DetalleCompras)
                        {
                            DETALLECOMPRAS Detalle = db.DETALLECOMPRAS.Where(x => x.DEC_CODIGO == item.DEC_CODIGO).FirstOrDefault();
                            int diferencia = 0;
                            if (Detalle != null)
                            {
                                //ACTUALIZAR PRODUCTOS
                                PRODUCTOS producto = db.PRODUCTOS.Where(x => x.PRO_CODIGO == item.PRO_CODIGO).FirstOrDefault();
                                if (producto != null)
                                {
                                    producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + item.DEC_UNIDADES;
                                    producto.PRO_ACTUALIZACION = DateTime.Now;
                                }
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void EliminarCompra(int com_codigo)
        {
            try
            {
                using (var db = dbContext)
                {
                    COMPRAS Compra = db.COMPRAS.Where(x => x.COM_CODIGO == com_codigo).FirstOrDefault()
                        ?? throw new Exception($"No Compras con el codigo de enlace {com_codigo}."); ;

                    TRANSACCIONES _transaccion = catalogoTransacciones.ListarTrasaccionPorIDEnlaceAsync(com_codigo.ToString(), TiposTransacciones.COMPRA).Result
                        ?? throw new Exception($"No hay Transacciones con el codigo de enlace {com_codigo}.");
                    if (_transaccion.TRA_CONFIRMADA)
                    {
                        catalogoTransacciones.AnularTrasaccionesPorIdEnlaceAsync(com_codigo.ToString(),TiposTransacciones.COMPRA).Wait();
                        Compra.COM_ESANULADA = true;
                        Compra.COM_FECHAACTUALIZACION = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        catalogoTransacciones.BorrarTrasaccionPorIdEnlaceAsync(com_codigo.ToString(), TiposTransacciones.COMPRA).Wait();
                        Compra.COM_ESTADO = false;
                        Compra.COM_FECHAACTUALIZACION = DateTime.Now;
                        db.SaveChanges();
                    }
                    //solo se devuelven los productos si ya ha sido ingresado a inventario
                    if (Compra.COM_ENBODEGA == true)
                    {
                        List<DETALLECOMPRAS> detalles = db.DETALLECOMPRAS.Where(x => x.COM_CODIGO == com_codigo && x.DEC_ESTADO == true).ToList();

                        foreach (DETALLECOMPRAS item in detalles)
                        {
                            int diferencia = -item.DEC_UNIDADES;
                            item.DEC_ESTADO = false;
                            item.DEC_FECHAACTUALIZACION = DateTime.Now;

                            //ACTUALIZAR PRODUCTOS
                            PRODUCTOS producto = db.PRODUCTOS.Where(x => x.PRO_CODIGO == item.PRO_CODIGO).FirstOrDefault();
                            if (producto != null)
                            {
                                producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + diferencia;
                                producto.PRO_ACTUALIZACION = DateTime.Now;
                            }
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void AnularCompra(int com_codigo)
        {
            try
            {
                using (var db = dbContext)
                {
                    COMPRAS Compra = db.COMPRAS.Where(x => x.COM_CODIGO == com_codigo).FirstOrDefault();
                    Compra.COM_ESANULADA = true;
                    Compra.COM_FECHAACTUALIZACION = DateTime.Now;
                    db.SaveChanges();
                    //solo se quitan unidades si ha estado en inventario
                    if (Compra.COM_ENBODEGA == true)
                    {
                        List<DETALLECOMPRAS> detalles = db.DETALLECOMPRAS.Where(x => x.COM_CODIGO == com_codigo && x.DEC_ESTADO == true).ToList();

                        foreach (DETALLECOMPRAS item in detalles)
                        {
                            int diferencia = -item.DEC_UNIDADES;
                            item.DEC_ESTADO = false;
                            item.DEC_FECHAACTUALIZACION = DateTime.Now;

                            //ACTUALIZAR PRODUCTOS
                            PRODUCTOS producto = db.PRODUCTOS.Where(x => x.PRO_CODIGO == item.PRO_CODIGO).FirstOrDefault();
                            if (producto != null)
                            {
                                producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + diferencia;
                                producto.PRO_ACTUALIZACION = DateTime.Now;
                            }
                        }
                    }
                    catalogoTransacciones.AnularTrasaccionesPorIdEnlaceAsync(com_codigo.ToString(), TiposTransacciones.COMPRA).Wait();
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ConfirmarCompra(int id)
        {
            try
            {
                var _compra = dbContext.COMPRAS.Find(id);
                if (_compra == null)
                    return;
                //_compra.COM_CREDITO = false;
                //dbContext.Entry(_compra).State = EntityState.Modified;
                //dbContext.SaveChanges();
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
