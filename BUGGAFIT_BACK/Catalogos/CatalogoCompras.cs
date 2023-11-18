using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.ConexionBD; // Asegúrate de agregar esta línea
using BUGGAFIT_BACK.Modelos.Entidad;
using BUGGAFIT_BACK.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoCompras : ICatalogoCompras
    {
        private MyDBContext dbContext;

        public CatalogoCompras(MyDBContext Context)
        {
            dbContext = Context;
        }

        List<Compra> ICatalogoCompras.ListarComprasPorFecha(FiltrosDTO filtro)
        {
            try
            {
                // Accede a la instancia de MyDBContext a través de ConexionBD 
                using (var db = dbContext)
                {
                    // Realiza consultas de Entity Framework aquí
                    List<Compra> compras = db.COMPRAS.Where(x => x.COM_FECHACOMPRA >= filtro.FechaInicio.ToLocalTime() && x.COM_FECHACOMPRA <= filtro.FechaFin.ToLocalTime() && x.COM_ESTADO==true).Select(x => new Compra
                    {
                        COM_CODIGO = x.COM_CODIGO,
                        COM_FECHACREACION = x.COM_FECHACREACION,
                        COM_FECHACOMPRA = x.COM_FECHACOMPRA,
                        COM_VALORCOMPRA = x.COM_VALORCOMPRA,
                        COM_PROVEEDOR = x.COM_PROVEEDOR,
                        TIC_CODIGO = x.TIC_CODIGO,
                        TIC_NOMBRE=x.TipoCuenta.TIC_NOMBRE,
                        COM_FECHAACTUALIZACION = x.COM_FECHAACTUALIZACION,
                        COM_ENBODEGA = x.COM_ENBODEGA,
                        COM_ESTADO = x.COM_ESTADO,
                        COM_CREDITO = x.COM_CREDITO,
                        USU_CEDULA = x.USU_CEDULA,
                        DetalleCompras=db.DETALLECOMPRAS.Where(d =>d.COM_CODIGO==x.COM_CODIGO &&d.DEC_ESTADO==true).Select(d=> new DetalleCompra
                        {
                            COM_CODIGO=d.COM_CODIGO,
                            DEC_CODIGO=d.DEC_CODIGO,
                            DEC_UNIDADES=d.DEC_UNIDADES,
                            DEC_PRECIOCOMPRA_PRODUCTO=d.DEC_PRECIOCOMPRA_PRODUCTO,
                            DEC_PRECIOTOTAL=d.DEC_PRECIOTOTAL,
                            DEC_ESTADO=d.DEC_ESTADO,
                            PRO_CODIGO=d.PRO_CODIGO,
                            PRO_NOMBRE=d.PRODUCTO.PRO_NOMBRE,
                        }).ToList(),
                    }).OrderByDescending(x =>x.COM_FECHACOMPRA).ToList();
                    return compras;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        Compra ICatalogoCompras.BuscarCompraPorID(int id)
        {
            try
            {
                
                using (var db = dbContext)
                {
                    COMPRAS Compras = db.COMPRAS.Where(x => x.COM_CODIGO == id).FirstOrDefault();
                    Compra compra=new Compra();
                    if(compra != null)
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
        void ICatalogoCompras.CrearCompra(Compra nuevaCompra)
        {
            try
            {

                using (var db = dbContext)
                {
                    COMPRAS compras= new COMPRAS();
                    bool pendiente = true;
                    if (db.TIPOSCUENTAS.Where(x => x.TIC_CODIGO == nuevaCompra.TIC_CODIGO).FirstOrDefault().TIC_NOMBRE.ToLower().Contains("efectivo") == true)
                    {
                        pendiente = false;
                    }
                    //compras.COM_CODIGO = nuevaCompra.COM_CODIGO;
                    compras.COM_FECHACREACION = DateTime.Now;
                        compras.COM_FECHACOMPRA = nuevaCompra.COM_FECHACOMPRA.ToLocalTime();
                        compras.COM_VALORCOMPRA = nuevaCompra.COM_VALORCOMPRA;
                        compras.COM_PROVEEDOR = nuevaCompra.COM_PROVEEDOR;
                        compras.TIC_CODIGO = nuevaCompra.TIC_CODIGO;
                        compras.COM_FECHAACTUALIZACION = DateTime.Now;
                        compras.COM_ENBODEGA = nuevaCompra.COM_ENBODEGA;
                        compras.COM_ESTADO = true;
                        compras.COM_CREDITO = pendiente;
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

                        //ACTUALIZAR PRODUCTOS
                        PRODUCTOS producto = db.PRODUCTOS.Where(x=>x.PRO_CODIGO==item.PRO_CODIGO).FirstOrDefault();
                        if (producto != null)
                        {
                            producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + item.DEC_UNIDADES;
                            producto.PRO_ACTUALIZACION = DateTime.Now;
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

        void ICatalogoCompras.ActualizarCompra(Compra nuevaCompra)
        {
            try
            {

                using (var db = dbContext)
                {
                    COMPRAS CompraAnterior = db.COMPRAS.Where(x => x.COM_CODIGO == nuevaCompra.COM_CODIGO).FirstOrDefault();
                    //compras.COM_CODIGO = nuevaCompra.COM_CODIGO;
                    CompraAnterior.COM_FECHACOMPRA = nuevaCompra.COM_FECHACOMPRA.ToLocalTime();
                    CompraAnterior.COM_VALORCOMPRA = nuevaCompra.COM_VALORCOMPRA;
                    CompraAnterior.COM_PROVEEDOR = nuevaCompra.COM_PROVEEDOR;
                    CompraAnterior.TIC_CODIGO = nuevaCompra.TIC_CODIGO;
                    CompraAnterior.COM_FECHAACTUALIZACION = DateTime.Now;
                    CompraAnterior.COM_ENBODEGA = nuevaCompra.COM_ENBODEGA;
                    CompraAnterior.COM_ESTADO = nuevaCompra.COM_ESTADO;
                    CompraAnterior.COM_CREDITO = CompraAnterior.COM_CREDITO;//no se actualiza
                    CompraAnterior.USU_CEDULA = nuevaCompra.USU_CEDULA;
                    db.SaveChanges();

                    foreach (DetalleCompra item in nuevaCompra.DetalleCompras)
                    {
                        DETALLECOMPRAS Detalle = db.DETALLECOMPRAS.Where(x => x.DEC_CODIGO == item.DEC_CODIGO).FirstOrDefault();
                        int diferencia = 0;
                        if (Detalle != null)
                        {
                            Detalle.PRO_CODIGO = item.PRO_CODIGO;
                            diferencia = item.DEC_UNIDADES - Detalle.DEC_UNIDADES;
                            Detalle.DEC_UNIDADES = item.DEC_UNIDADES;
                            Detalle.DEC_PRECIOCOMPRA_PRODUCTO = item.DEC_PRECIOCOMPRA_PRODUCTO;
                            Detalle.DEC_PRECIOTOTAL = item.DEC_PRECIOTOTAL;
                            Detalle.DEC_FECHACREACION = DateTime.Now;
                            Detalle.DEC_FECHAACTUALIZACION = DateTime.Now;
                            Detalle.DEC_ESTADO = item.DEC_ESTADO;
                        }
                        else
                        {
                            Detalle = new DETALLECOMPRAS();
                            Detalle.COM_CODIGO = nuevaCompra.COM_CODIGO;
                            Detalle.PRO_CODIGO = item.PRO_CODIGO;
                            Detalle.DEC_UNIDADES = item.DEC_UNIDADES;
                            Detalle.DEC_PRECIOCOMPRA_PRODUCTO = item.DEC_PRECIOCOMPRA_PRODUCTO;
                            Detalle.DEC_PRECIOTOTAL = item.DEC_PRECIOTOTAL;
                            Detalle.DEC_FECHACREACION = DateTime.Now;
                            Detalle.DEC_FECHAACTUALIZACION = DateTime.Now;
                            Detalle.DEC_ESTADO =true;
                            diferencia = item.DEC_UNIDADES;
                            db.DETALLECOMPRAS.Add(Detalle);
                        }




                        //ACTUALIZAR PRODUCTOS
                        PRODUCTOS producto = db.PRODUCTOS.Where(x => x.PRO_CODIGO == item.PRO_CODIGO).FirstOrDefault();
                        if (producto != null)
                        {
                            producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + diferencia;
                            if (item.DEC_ESTADO == false)
                            {
                                producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES - item.DEC_UNIDADES;
                            }
                            producto.PRO_ACTUALIZACION = DateTime.Now;
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

        void ICatalogoCompras.EliminarCompra(int com_codigo)
        {
            try
            {

                using (var db = dbContext)
                {
                    COMPRAS Compra = db.COMPRAS.Where(x => x.COM_CODIGO == com_codigo).FirstOrDefault();
                    Compra.COM_ESTADO = false;
                    Compra.COM_FECHAACTUALIZACION = DateTime.Now;
                    db.SaveChanges();
                    List< DETALLECOMPRAS> detalles = db.DETALLECOMPRAS.Where(x =>x.COM_CODIGO==com_codigo && x.DEC_ESTADO==true).ToList(); 

                    foreach (DETALLECOMPRAS item in detalles)
                    {
                            int diferencia = - item.DEC_UNIDADES;
                            item.DEC_ESTADO = false;
                            item.DEC_FECHAACTUALIZACION=DateTime.Now;

                        //ACTUALIZAR PRODUCTOS
                        PRODUCTOS producto = db.PRODUCTOS.Where(x => x.PRO_CODIGO == item.PRO_CODIGO).FirstOrDefault();
                        if (producto != null)
                        {
                            producto.PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES + diferencia;
                            producto.PRO_ACTUALIZACION = DateTime.Now;
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
    }
}
