using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoGastos
    {
        // Async Methods
        Task<ResponseObject> CrearGastoAsync(Gasto gasto);
        Task<ResponseObject> CrearGastoVentaAsync(Gasto gasto);
        Task<ResponseObject> ListarGastosAsync();
        Task<ResponseObject> ActualizarGastoAsync(Gasto gasto);
        Task<ResponseObject> BorrarGastoAsync(int Id);
        Task<ResponseObject> ListarGastoPorIDAsync(int Id);
        Task<ResponseObject> ListarMotivoGastosDeEnvioAsync();
        Task<ResponseObject> CerrarGasto(int id);


    }
}
