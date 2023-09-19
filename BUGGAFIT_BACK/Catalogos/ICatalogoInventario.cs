using BUGGAFIT_BACK.Clases;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoInventario
    {
        Producto ListarProductoPorID(int Id);
        List<Producto> ListarProductos();
        Producto CrearProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void BorrarProducto(int Id);

        // Async Methods
        Task<Producto> CrearProductoAsync(Producto producto);
        Task<List<Producto>> ListarProductosAsync();
        Task ActualizarProductoAsync(Producto producto);
        Task BorrarProductoAsync(int Id);
        Task<Producto> ListarProductoPorIDAsync(int Id);
    }
}
