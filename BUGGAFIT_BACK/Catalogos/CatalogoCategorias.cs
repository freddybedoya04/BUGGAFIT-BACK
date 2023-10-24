using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoCategorias:ICatalogoCategorias
    {
        private readonly MyDBContext myDbContext;

        public CatalogoCategorias(MyDBContext context)
        {
            myDbContext = context;
        }
        public async Task<ResponseObject> ListarCategoriasAsync()
        {
            try
            {
                var _categorias = await myDbContext.CATEGORIAS.Where(x => x.CAT_ESTADO == true).OrderBy(x => x.CAT_NOMBRE).ToListAsync();
                if (_categorias == null || !_categorias.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay Marcas.");

                return ResponseClass.Response(statusCode: 200, data: _categorias);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
