using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.ConexionBD; // Asegúrate de agregar esta línea
using BUGGAFIT_BACK.Modelos.Entidad;
using BUGGAFIT_BACK.Modelos;

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
                    List<Compra> compras = db.COMPRAS.Where(x => x.COM_FECHACOMPRA >= filtro.FechaInicio && x.COM_FECHACOMPRA <= filtro.FechaFin).Select(x => new Compra
                    {
                        COM_CODIGO = x.COM_CODIGO,
                        COM_FECHACREACION = x.COM_FECHACREACION,
                        COM_FECHACOMPRA = x.COM_FECHACOMPRA,
                        COM_VALORCOMPRA = x.COM_VALORCOMPRA,
                        COM_PROVEEDOR = x.COM_PROVEEDOR,
                        TIC_CODIGO = x.TIC_CODIGO,
                        COM_FECHAACTUALIZACION = x.COM_FECHAACTUALIZACION,
                        COM_ENBODEGA = x.COM_ENBODEGA,
                        COM_ESTADO = x.COM_ESTADO,
                        COM_CREDITO = x.COM_CREDITO,
                        USU_CEDULA = x.USU_CEDULA
                    }).ToList();
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
                    
                        //compras.COM_CODIGO = nuevaCompra.COM_CODIGO;
                        compras.COM_FECHACREACION = DateTime.Now;
                        compras.COM_FECHACOMPRA = nuevaCompra.COM_FECHACOMPRA;
                        compras.COM_VALORCOMPRA = nuevaCompra.COM_VALORCOMPRA;
                        compras.COM_PROVEEDOR = nuevaCompra.COM_PROVEEDOR;
                        compras.TIC_CODIGO = nuevaCompra.TIC_CODIGO;
                        compras.COM_FECHAACTUALIZACION = nuevaCompra.COM_FECHAACTUALIZACION;
                        compras.COM_ENBODEGA = nuevaCompra.COM_ENBODEGA;
                        compras.COM_ESTADO = nuevaCompra.COM_ESTADO;
                        compras.COM_CREDITO = nuevaCompra.COM_CREDITO;
                        compras.USU_CEDULA = nuevaCompra.USU_CEDULA;
                        
                    db.COMPRAS.Add(compras);
                    
                    //crear detalles
                    foreach (DetalleCompra item in nuevaCompra.DetalleCompras)
                    {
                        DETALLECOMPRAS Detalle = new DETALLECOMPRAS();
                        Detalle.DEC_CODIGO = item.DEC_CODIGO;
                        Detalle.COM_CODIGO = compras.COM_CODIGO;
                        Detalle.PRO_CODIGO = item.PRO_CODIGO;
                        Detalle.DEC_UNIDADES = item.DEC_UNIDADES;
                        Detalle.DEC_PRECIOCOMPRA_PRODUCTO = item.DEC_PRECIOCOMPRA_PRODUCTO;
                        Detalle.DEC_PRECIOTOTAL = item.DEC_PRECIOTOTAL;
                        Detalle.DEC_FECHACREACION = DateTime.Now;
                        Detalle.DEC_FECHAACTUALIZACION = item.DEC_FECHAACTUALIZACION;
                        Detalle.DEC_ESTADO = item.DEC_ESTADO;

                        db.DETALLECOMPRAS.Add(Detalle);
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
