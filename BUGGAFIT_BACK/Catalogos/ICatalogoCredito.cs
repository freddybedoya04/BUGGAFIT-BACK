using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoCredito
    {
        Task<ResponseObject> ListarCreditosAsync();
        Task<ResponseObject> ListarCreditosPorClienteAsync(string id);

    }
}
