using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoMarcas:ICatalogoMarcas
    {
        private readonly MyDBContext myDbContext;

        public CatalogoMarcas(MyDBContext context)
        {
            myDbContext = context;
        }
        public async Task<ResponseObject> ListarMarcasAsync()
        {
            try
            {
                var _marcas = await myDbContext.MARCAS.Where(x => x.MAR_ESTADO == true).OrderBy(x => x.MAR_NOMBRE).ToListAsync();
                if (_marcas == null || !_marcas.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay categorias.");

                return ResponseClass.Response(statusCode: 200, data: _marcas);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearMarcaAsync(Marca marca)
        {
            try
            {
                MARCAS _marca = new MARCAS() {
                    MAR_NOMBRE = marca.MAR_NOMBRE,
                    MAR_FECHACREACION = DateTime.Now,
                    MAR_ESTADO = true,
                };

                myDbContext.MARCAS.Add(_marca);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Producto Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> BorrarMarcaAsync(int id)
        {
            try
            {
                var marca = await myDbContext.MARCAS.FindAsync(id);
                if (marca == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El abono con el codigo {id} no existe.");

                marca.MAR_ESTADO = false;
                myDbContext.Entry(marca).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Abono Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
