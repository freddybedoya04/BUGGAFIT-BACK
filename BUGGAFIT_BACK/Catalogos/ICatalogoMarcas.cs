using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoMarcas
    {
        Task<ResponseObject> ListarMarcasAsync();
        Task<ResponseObject> CrearMarcaAsync(Marca marca);
        Task<ResponseObject> BorrarMarcaAsync(int id);
    }
}
