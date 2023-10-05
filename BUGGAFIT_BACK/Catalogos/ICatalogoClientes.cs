using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoClientes
    {
        Task<ResponseObject> CrearClienteAsync(Cliente cliente);
        Task<ResponseObject> ListarClientesAsync();
        Task<ResponseObject> ActualizarClienteAsync(Cliente cliente);
        Task<ResponseObject> BorrarClienteAsync(string Id);
        Task<ResponseObject> ListarClientePorIDAsync(string Id);
    }
}
