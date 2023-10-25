using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoCategorias
    {
        Task<ResponseObject> ListarCategoriasAsync();
        Task<ResponseObject> CrearCategoriaAsync(Categoria categoria);
        Task<ResponseObject> BorrarCategoriaAsync(int id);
    }
}
