using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.ConexionBD; // Asegúrate de agregar esta línea

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoCompras : ICatalogoCompras
    {

        List<Compra> ICatalogoCompras.ListarComprasPorFecha(FiltrosDTO filtro)
        {
            try
            {
                // Accede a la instancia de MyDBContext a través de ConexionBD
                using (var dbContext = ConexionBD.ConexionBD.Instance)
                {
                    // Realiza consultas de Entity Framework aquí
                    List<Compra> compras = dbContext.COMPRAS.Where(x => x.COM_FECHACOMPRA >= filtro.FechaInicio && x.COM_FECHACOMPRA <= filtro.FechaFin).Select(x => new Compra
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
    }
}
