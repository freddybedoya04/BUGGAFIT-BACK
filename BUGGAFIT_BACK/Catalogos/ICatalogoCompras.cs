using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoCompras
    {
        List<Compra> ListarComprasPorFecha(FiltrosDTO filtro);
        Compra BuscarCompraPorID(int id);
        void CrearCompra(Compra nuevaCompra);
        void ActualizarCompra(Compra nuevaCompra);
        void EliminarCompra(int com_codigo);
    }
}
