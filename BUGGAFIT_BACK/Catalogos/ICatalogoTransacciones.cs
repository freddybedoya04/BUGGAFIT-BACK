using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoTransacciones
    {
        Task<ResponseObject> CrearTrasaccionAsync(Transacciones transaccion);
        Task<ResponseObject> ListarTrasaccionesAsync();
        Task<ResponseObject> AnularTrasaccionesAsync(int Id);
        Task<ResponseObject> ConfirmarTrasaccionesAsync(int Id);
        Task<ResponseObject> ListarTrasaccionesPorFechaAsync(FiltrosDTO filtro);
        Task<ResponseObject> ActualizarTrasaccionAsync(Transacciones transaccion);
        Task<ResponseObject> BorrarTrasaccionAsync(int Id);
        Task<ResponseObject> ListarTrasaccionPorIDAsync(int Id);
        Task<ResponseObject> BorrarTrasaccionPorIdEnlaceAsync(string idEnlace);
        Task<ResponseObject> AnularTrasaccionesPorIdEnlaceAsync(string idEnlace);
        Task<ResponseObject> ListarTrasaccionPorIDEnlaceAsync(string idEnlace);

    }
}
