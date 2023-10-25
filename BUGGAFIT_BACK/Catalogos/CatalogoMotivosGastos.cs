using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos.Entidad;
using BUGGAFIT_BACK.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoMotivosGastos:ICatalogoMotivosGastos
    {
        private readonly MyDBContext myDbContext;

        public CatalogoMotivosGastos(MyDBContext context)
        {
            myDbContext = context;
        }
        public async Task<ResponseObject> ListarMotivoGastoAsync()
        {
            try
            {
                var _motivos = await myDbContext.MOTIVOSGASTOS.Where(x => x.MOG_ESTADO == true).OrderBy(x => x.MOG_NOMBRE).ToListAsync();
                if (_motivos == null || !_motivos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay MOTIVOS.");

                return ResponseClass.Response(statusCode: 200, data: _motivos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearMotivoGastoAsync(MotivoGasto motivo)
        {
            try
            {
                MOTIVOSGASTOS _motivos = new MOTIVOSGASTOS()
                {
                    MOG_NOMBRE = motivo.MOG_NOMBRE,
                    MOG_FECHACREACION = DateTime.Now,
                    MOG_ESTADO = true,
                };

                myDbContext.MOTIVOSGASTOS.Add(_motivos);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"MOTIVO Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObject> BorrarMotivoGastoAsync(int id)
        {
            try
            {
                var motivo = await myDbContext.MOTIVOSGASTOS.FindAsync(id);
                if (motivo == null)
                    return ResponseClass.Response(statusCode: 400, message: $"el motivo con el codigo {id} no existe.");

                motivo.MOG_ESTADO = false;
                myDbContext.Entry(motivo).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Motivo Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
