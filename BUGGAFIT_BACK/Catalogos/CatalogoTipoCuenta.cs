using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoTipoCuenta : ICatalogoTipoCuenta
    {
        private readonly MyDBContext myDbContext;

        public CatalogoTipoCuenta(MyDBContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<ResponseObject> ActualizarTipoCuentaAsync(TipoCuenta tipoCuenta)
        {
            ArgumentNullException.ThrowIfNull(tipoCuenta.TIC_CODIGO, nameof(tipoCuenta));
            try
            {
                TIPOSCUENTAS _tipoCuenta = new()
                {
                    TIC_NOMBRE = tipoCuenta.TIC_NOMBRE,
                    TIC_NUMEROREFERENCIA = tipoCuenta.TIC_NUMEROREFERENCIA,
                    TIC_ESTADO = tipoCuenta.TIC_ESTADO,
                };
                myDbContext.Entry(_tipoCuenta).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteTipoCuenta(tipoCuenta.TIC_CODIGO))
                    return ResponseClass.Response(statusCode: 400, message: $"La cuenta con el codigo {tipoCuenta.TIC_CODIGO} no existe.");

                throw;
            }
            catch (Exception) { throw; }
            return ResponseClass.Response(statusCode: 204, message: $"Cuenta Actualizada Exitosamente.");
        }

        public async Task<ResponseObject> BorrarTipoCuentaAsync(int Id)
        {
            try
            {
                var _tipoCuenta = await myDbContext.TIPOSCUENTAS.FindAsync(Id);
                if (_tipoCuenta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La cuenta con el codigo {Id} no existe.");

                _tipoCuenta.TIC_ESTADO = false;
                myDbContext.Entry(_tipoCuenta).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Cuenta Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> RemoverTipoCuentaAsync(int Id)
        {
            try
            {
                var _tipoCuenta = await myDbContext.TIPOSCUENTAS.FindAsync(Id);
                if (_tipoCuenta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La cuenta con el codigo {Id} no existe.");

                myDbContext.TIPOSCUENTAS.Remove(_tipoCuenta);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Cuenta Eliminada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearTipoCuentaAsync(TipoCuenta tipoCuenta)
        {
            try
            {
                TIPOSCUENTAS _tipoCuentas = new()
                {
                    TIC_NOMBRE = tipoCuenta.TIC_NOMBRE,
                    TIC_NUMEROREFERENCIA = tipoCuenta.TIC_NUMEROREFERENCIA,
                    TIC_ESTADO =true,
                    TIC_FECHACREACION = DateTime.Now,
                };
                myDbContext.TIPOSCUENTAS.Add(_tipoCuentas);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Cuenta Creada Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarTipoCuentaPorIDAsync(int Id)
        {
            try
            {
                var _tipoCuenta = await myDbContext.TIPOSCUENTAS.FindAsync(Id);
                if (_tipoCuenta == null)
                    return ResponseClass.Response(statusCode: 400, message: $"La cuenta con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: _tipoCuenta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarTipoCuentasAsync()
        {
            try
            {
                var _tipoCuenta = await myDbContext.TIPOSCUENTAS.Where(x => x.TIC_ESTADO == true).ToListAsync();
                if (_tipoCuenta == null || !_tipoCuenta.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay Cuentas.");

                return ResponseClass.Response(statusCode: 200, data: _tipoCuenta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ExisteTipoCuenta(int id)
        {
            return myDbContext.TIPOSCUENTAS.Any(e => e.TIC_CODIGO == id);
        }
    }
}
