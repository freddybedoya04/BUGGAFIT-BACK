using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoTiposEnvios:ICatalogoTiposEnvios   
    {
        private readonly MyDBContext myDbContext;

        public CatalogoTiposEnvios(MyDBContext context)
        {
            myDbContext = context;
        }

        public async Task<ResponseObject> ListarTiposEnviosAsync()
        {
            try
            {
                var _tipoenvio = await myDbContext.TIPOSENVIOS.Where(x => x.TIP_ESTADO == true).OrderBy(x => x.TIP_NOMBRE).ToListAsync();
                if (_tipoenvio == null || !_tipoenvio.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay MOTIVOS.");

                return ResponseClass.Response(statusCode: 200, data: _tipoenvio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearTiposEnviosAsync(TiposEnvios tipoEnvio)
        {
            try
            {
                TIPOSENVIOS _tipoenvio = new TIPOSENVIOS()
                {
                    TIP_NOMBRE = tipoEnvio.TIP_NOMBRE,
                    TIP_TIMESPAN = DateTime.Now,
                    TIP_VALOR= tipoEnvio.TIP_VALOR,
                    TIP_ESTADO = true,
                };

                myDbContext.TIPOSENVIOS.Add(_tipoenvio);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"TIPOENVIO Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> ActualizarTiposEnviosAsync(TiposEnvios _tipoEnvio)
        {
            try
            {
                var tipoenvio = await myDbContext.TIPOSENVIOS.FindAsync(_tipoEnvio.TIP_CODIGO);
                if (tipoenvio == null)
                    return ResponseClass.Response(statusCode: 400, message: $"el tipo envio con el codigo {_tipoEnvio.TIP_CODIGO} no existe.");

                tipoenvio.TIP_VALOR = _tipoEnvio.TIP_VALOR;
                tipoenvio.TIP_TIMESPAN = DateTime.Now;
                myDbContext.Entry(tipoenvio).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Tipo envio Actualizado Exitosamente.");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> BorrarTiposEnviosAsync(int id)
        {
            try
            {
                var tipoenvio = await myDbContext.TIPOSENVIOS.FindAsync(id);
                if (tipoenvio == null)
                    return ResponseClass.Response(statusCode: 400, message: $"el tipo envio con el codigo {id} no existe.");

                tipoenvio.TIP_ESTADO = false;
                myDbContext.Entry(tipoenvio).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Tipo envio Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
