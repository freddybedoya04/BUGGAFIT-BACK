using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoTransacciones : ICatalogoTransacciones
    {
        private readonly MyDBContext myDbContext;

        public CatalogoTransacciones(MyDBContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<ResponseObject> ActualizarTrasaccionAsync(Transacciones transaccion)
        {
            ArgumentNullException.ThrowIfNull(transaccion.TRA_CODIGO, nameof(transaccion));
            try
            {
                TRANSACCIONES _transaccion = new()
                {
                    TRA_CODIGO = transaccion.TRA_CODIGO,
                    TIC_CUENTA = transaccion.TIC_CUENTA,
                    TRA_TIPO = transaccion.TRA_TIPO,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = transaccion.TRA_CONFIRMADA,
                    TRA_ESTADO = transaccion.TRA_ESTADO,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = transaccion.TRA_CODIGOENLACE,
                    TRA_FUEANULADA = transaccion.TRA_FUEANULADA,
                    TRA_NUMEROTRANSACCIONBANCO = transaccion.TRA_NUMEROTRANSACCIONBANCO,
                    USU_CEDULA_CONFIRMADOR = transaccion.USU_CEDULA_CONFIRMADOR,
                };
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteTrasaccion(transaccion.TRA_CODIGO))
                    return ResponseClass.Response(statusCode: 400, message: $"La Transaccion con el codigo {transaccion.TRA_CODIGO} no existe.");

                throw;
            }
            catch (Exception) { throw; }
            return ResponseClass.Response(statusCode: 204, message: $"Transaccion Actualizada Exitosamente.");
        }

        public async Task<ResponseObject> AnularTrasaccionesAsync(int Id)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.FindAsync(Id);
                if (_transaccion == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La transaccion con el codigo {Id} no existe.");

                _transaccion.TRA_FUEANULADA = true;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> BorrarTrasaccionAsync(int Id)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.FindAsync(Id);
                if (_transaccion == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La transaccion con el codigo {Id} no existe.");

                _transaccion.TRA_ESTADO = false;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ConfirmarTrasaccionesAsync(int Id)
        {
            try
            {
                var _transaccion = await myDbContext.TRANSACCIONES.FindAsync(Id);
                if (_transaccion == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La transaccion con el codigo {Id} no existe.");

                _transaccion.TRA_CONFIRMADA = true;
                myDbContext.Entry(_transaccion).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Transaccion Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearTrasaccionAsync(Transacciones transaccion)
        {
            try
            {
                TRANSACCIONES _transaccion = new()
                {
                    TIC_CUENTA = transaccion.TIC_CUENTA,
                    TRA_TIPO = transaccion.TRA_TIPO,
                    TRA_FECHACREACION = DateTime.Now,
                    TRA_CONFIRMADA = transaccion.TRA_CONFIRMADA,
                    TRA_ESTADO = transaccion.TRA_ESTADO,
                    TRA_FECHACONFIRMACION = DateTime.Now,
                    TRA_CODIGOENLACE = transaccion.TRA_CODIGOENLACE,
                    TRA_FUEANULADA = transaccion.TRA_FUEANULADA,
                    TRA_NUMEROTRANSACCIONBANCO = transaccion.TRA_NUMEROTRANSACCIONBANCO,
                    USU_CEDULA_CONFIRMADOR = transaccion.USU_CEDULA_CONFIRMADOR,
                };
                myDbContext.TRANSACCIONES.Add(_transaccion);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Transaccion Creada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarTrasaccionesAsync()
        {
            try
            {
                var _transacciones = await myDbContext.TRANSACCIONES.Where(x => x.TRA_ESTADO == true).ToListAsync();
                if (_transacciones == null || !_transacciones.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay transacciones.");

                return ResponseClass.Response(statusCode: 200, data: _transacciones);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarTrasaccionesPorFechaAsync(FiltrosDTO filtro)
        {

            try
            {
                var _transacciones = await myDbContext.TRANSACCIONES
                    .Where(x => x.TRA_FECHACREACION >= filtro.FechaInicio.ToLocalTime() && x.TRA_FECHACREACION <= filtro.FechaFin.ToLocalTime() && x.TRA_ESTADO == true)
                    .ToListAsync();
                if (_transacciones == null || !_transacciones.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay transacciones.");

                return ResponseClass.Response(statusCode: 200, data: _transacciones);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarTrasaccionPorIDAsync(int Id)
        {
            try
            {
                var _tipoCuenta = await myDbContext.TRANSACCIONES.Where(x => x.TRA_ESTADO == true && x.TRA_CODIGO == Id).FirstOrDefaultAsync();
                if (_tipoCuenta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La transaccion con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: _tipoCuenta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ExisteTrasaccion(int id)
        {
            return myDbContext.TIPOSCUENTAS.Any(e => e.TIC_CODIGO == id);
        }
    }
}
