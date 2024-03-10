using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoMotivosGastos
    {
        Task<ResponseObject> ListarMotivoGastoAsync();
        Task<ResponseObject> CrearMotivoGastoAsync(MotivoGasto motivo);
        Task<ResponseObject> BorrarMotivoGastoAsync(int id);
        Task<ResponseObject> ActualizarMotivoGastoAsync(MotivoGasto motivo);
    }
}
