using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly ICatalogoCredito catalogo;

        public CreditoController(ICatalogoCredito catalogo)
        {
            this.catalogo = catalogo;
        }

        #region API
        [HttpGet("GetCreditos")]
        public async Task<ActionResult<ResponseObject>> GetCreditos()
        {
            try
            {
                var result = await catalogo.ListarCreditosAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpGet("GetCreditoCliente/{id}")]
        public async Task<ActionResult<ResponseObject>> GetCreditoCliente(string id)
        {
            try
            {
                var result = await catalogo.ListarCreditosPorClienteAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        #endregion
    }
}
