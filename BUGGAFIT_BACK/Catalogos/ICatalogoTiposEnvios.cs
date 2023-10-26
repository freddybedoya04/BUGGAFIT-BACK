using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoTiposEnvios
    {
        Task<ResponseObject> ListarTiposEnviosAsync();
        Task<ResponseObject> CrearTiposEnviosAsync(TiposEnvios tiposEnvios);
        Task<ResponseObject> ActualizarTiposEnviosAsync(TiposEnvios tiposEnvios);
        Task<ResponseObject> BorrarTiposEnviosAsync(int id);
    }
}
