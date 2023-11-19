using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoVentas
    {
        // Async Methods
        Task<ResponseObject> CrearVentaAsync(Ventas venta);
        Task<ResponseObject> CrearDetalleVentaAsync(List<DetalleVenta> detalleVentas);
        Task<ResponseObject> ListarVentasAsync();
        Task<ResponseObject> ActualizarVentaAsync(Ventas venta);
        Task<ResponseObject> BorrarVentaAsync(int Id);
        Task<ResponseObject> AnularVentaAsync(int Id);
        Task<ResponseObject> ListarVentaPorIDAsync(int Id);
        Task<ResponseObject> ListarVentasPorFechaAsync(FiltrosDTO filtro);
        Task<ResponseObject> ActualizarEstadoVentaAsync(int id);
        Task<ResponseObject> ListarDetallePorCodigoVentaAsync(int id);
        Task<ResponseObject> ListarAbonosPorCodigoVentaAsync(int id);
        Task<ResponseObject> CrearAbonoAsync(Cartera cartera);
        Task<ResponseObject> BorrarAbonoAsync(int car_codigo);
        Task<ResponseObject> ActualizarAbonoAsync(Cartera cartera);
        Task<ResponseObject> FinalizarCreditoAsync(int id);
    }
}
