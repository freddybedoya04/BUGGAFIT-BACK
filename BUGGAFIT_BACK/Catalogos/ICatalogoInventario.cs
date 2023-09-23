using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoInventario
    {
        // Async Methods
        Task<ResponseObject> CrearProductoAsync(Producto producto);
        Task<ResponseObject> ListarProductosAsync();
        Task<ResponseObject> ActualizarProductoAsync(Producto producto);
        Task<ResponseObject> BorrarProductoAsync(int Id);
        Task<ResponseObject> ListarProductoPorIDAsync(int Id);
    }
}
