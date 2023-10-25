using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
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

        public async Task<ResponseObject> CrearCategoriaAsync(Categoria categoria)
        {
            try
            {
                CATEGORIAS _categoria = new CATEGORIAS()
                {
                    CAT_NOMBRE = categoria.CAT_NOMBRE,
                    CAT_FECHACREACION = DateTime.Now,
                    CAT_ESTADO = true,
                };

                myDbContext.CATEGORIAS.Add(_categoria);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Categoria Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> BorrarCategoriaAsync(int id)
        {
            try
            {
                var categoria = await myDbContext.CATEGORIAS.FindAsync(id);
                if (categoria == null)
                    return ResponseClass.Response(statusCode: 400, message: $"la categoria con el codigo {id} no existe.");

                categoria.CAT_ESTADO = false;
                myDbContext.Entry(categoria).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Categoria Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
