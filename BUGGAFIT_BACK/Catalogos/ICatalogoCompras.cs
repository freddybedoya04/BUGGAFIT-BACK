using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoCompras
    {
        List<Compra> ListarComprasPorFecha(FiltrosDTO filtro);
       
    }
}
