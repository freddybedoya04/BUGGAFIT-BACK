using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoGastos : ICatalogoGastos
    {
        private readonly MyDBContext myDbContext;

        public CatalogoGastos(MyDBContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<ResponseObject> ActualizarGastoAsync(Gasto gasto)
        {
            ArgumentNullException.ThrowIfNull(gasto.GAS_CODIGO, nameof(gasto));
            try
            {
                GASTOS _gasto = new()
                {
                    GAS_CODIGO = gasto.GAS_CODIGO,
                    GAS_FECHAGASTO = gasto.GAS_FECHAGASTO,
                    MOG_CODIGO = gasto.MOG_CODIGO,
                    GAS_VALOR = gasto.GAS_VALOR,
                    TIC_CODIGO = gasto.TIC_CODIGO,
                    GAS_ESTADO = gasto.GAS_ESTADO,
                    USU_CEDULA = gasto.USU_CEDULA,
                    GAS_PENDIENTE = gasto.GAS_PENDIENTE,
                    VEN_CODIGO = gasto.VEN_CODIGO,
                };
                myDbContext.Entry(_gasto).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteGasto(gasto.GAS_CODIGO))
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {gasto.GAS_CODIGO} no existe.");
                throw;
            }
            catch (Exception) { throw; }
            return ResponseClass.Response(statusCode: 204, message: $"Gasto Actualizado Exitosamente.");
        }

        public async Task<ResponseObject> BorrarGastoAsync(int Id)
        {
            try
            {
                var _gasto = await myDbContext.GASTOS.FindAsync(Id);
                if (_gasto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {Id} no existe.");

                myDbContext.GASTOS.Remove(_gasto);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Gasto Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearGastoAsync(Gasto gasto)
        {
            try
            {
                GASTOS _gasto = new()
                {
                    GAS_CODIGO = gasto.GAS_CODIGO,
                    GAS_FECHACREACION = gasto.GAS_FECHACREACION,
                    GAS_FECHAGASTO = gasto.GAS_FECHAGASTO,
                    MOG_CODIGO = gasto.MOG_CODIGO,
                    GAS_VALOR = gasto.GAS_VALOR,
                    TIC_CODIGO = gasto.TIC_CODIGO,
                    GAS_ESTADO = gasto.GAS_ESTADO,
                    USU_CEDULA = gasto.USU_CEDULA,
                    GAS_PENDIENTE = gasto.GAS_PENDIENTE,
                    VEN_CODIGO = gasto.VEN_CODIGO,
                };
                myDbContext.GASTOS.Add(_gasto);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Gasto Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarGastoPorIDAsync(int Id)
        {
            try
            {
                var _producto = await myDbContext.GASTOS.FindAsync(Id);
                if (_producto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El gasto con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: _producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarGastosAsync()
        {
            try
            {
                var _gastos = await myDbContext.GASTOS.Where(x => x.GAS_ESTADO == true).ToListAsync();
                if (_gastos == null || !_gastos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay gastos.");

                return ResponseClass.Response(statusCode: 200, data: _gastos);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool ExisteGasto(int id)
        {
            return myDbContext.GASTOS.Any(e => e.GAS_CODIGO == id);
        }
    }
}
