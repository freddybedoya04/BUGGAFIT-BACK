using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoTipoCuenta
    {
        Task<ResponseObject> CrearTipoCuentaAsync(TipoCuenta tipoCuenta);
        Task<ResponseObject> ListarTipoCuentasAsync();
        Task<ResponseObject> ActualizarTipoCuentaAsync(TipoCuenta tipoCuenta);
        Task<ResponseObject> BorrarTipoCuentaAsync(int Id);
        Task<ResponseObject> ListarTipoCuentaPorIDAsync(int Id);
    }
}
