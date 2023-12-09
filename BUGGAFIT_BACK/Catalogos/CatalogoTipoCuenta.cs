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
        private readonly ICatalogoTransacciones? catalogoTransacciones;

        public CatalogoTipoCuenta(MyDBContext myDbContext, ICatalogoTransacciones? catalogoTransacciones)
        {
            this.myDbContext = myDbContext;
            this.catalogoTransacciones = catalogoTransacciones;
        }

        public async Task<ResponseObject> ActualizarTipoCuentaAsync(TipoCuenta tipoCuenta)
        {
            ArgumentNullException.ThrowIfNull(tipoCuenta.TIC_CODIGO, nameof(tipoCuenta));
            try
            {   

                TIPOSCUENTAS _tipoCuenta = myDbContext.TIPOSCUENTAS.Where(x=>x.TIC_CODIGO==tipoCuenta.TIC_CODIGO).FirstOrDefault();
                float diferencia = (float)(tipoCuenta.TIC_DINEROTOTAL - _tipoCuenta.TIC_DINEROTOTAL); 
                _tipoCuenta.TIC_NOMBRE = tipoCuenta.TIC_NOMBRE;
                _tipoCuenta.TIC_NUMEROREFERENCIA = tipoCuenta.TIC_NUMEROREFERENCIA;
                _tipoCuenta.TIC_ESTADO = tipoCuenta.TIC_ESTADO;
                _tipoCuenta.TIC_ESTIPOENVIO = tipoCuenta.TIC_ESTIPOENVIO;
                //_tipoCuenta.TIC_DINEROTOTAL = tipoCuenta.TIC_DINEROTOTAL;
                myDbContext.Entry(_tipoCuenta).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                if (diferencia !=0)
                {
                    var tipoTransaccion = TiposTransacciones.MOVIMIENTO;
                    await catalogoTransacciones.CrearTrasaccionAsync(new()
                    {
                        TIC_CODIGO = _tipoCuenta.TIC_CODIGO,
                        TRA_TIPO = tipoTransaccion.Nombre,
                        TRA_FECHACREACION = DateTime.Now,
                        TRA_CONFIRMADA = true,
                        TRA_ESTADO = true,
                        TRA_FECHACONFIRMACION =DateTime.Now ,
                        TRA_CODIGOENLACE = null,
                        TRA_FUEANULADA = false,
                        TRA_NUMEROTRANSACCIONBANCO = 0,
                        USU_CEDULA_CONFIRMADOR = null,
                        TRA_VALOR = diferencia,
                    }); 
                }
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
                    TIC_DINEROTOTAL = tipoCuenta.TIC_DINEROTOTAL,
                    TIC_FECHACREACION = DateTime.Now,
                    TIC_ESTIPOENVIO=tipoCuenta.TIC_ESTIPOENVIO
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
