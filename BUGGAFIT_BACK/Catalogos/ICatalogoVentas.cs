using BUGGAFIT_BACK.Clases;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoVentas
    {
        Ventas ListarVentaPorID(int Id);
        List<Ventas> ListarVentas();
        Ventas CrearVenta(Ventas venta);
        void ActualizarVenta(Ventas venta);
        void BorrarVenta(int Id);

        // Async Methods
        Task<Ventas> CrearVentaAsync(Ventas venta);
        Task<List<Ventas>> ListarVentasAsync();
        Task ActualizarVentaAsync(Ventas venta);
        Task BorrarVentaAsync(int Id);
        Task<Producto> ListarVentaPorIDAsync(int Id);
    }
}
