using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoCategorias
    {
        Task<ResponseObject> ListarCategoriasAsync();
    }
}
