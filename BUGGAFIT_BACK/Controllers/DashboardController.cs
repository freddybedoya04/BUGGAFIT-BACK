using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ICatalogoDashboard catalogo;

        public DashboardController(ICatalogoDashboard catalgo)
        {
            this.catalogo = catalgo;
        }

        // POST api/<ComprasController>
        [HttpPost("GetDashboard")]
        public async Task<IActionResult> GetDashboard([FromBody] FiltrosDTO filtro)
        {
            try
            {
                var result = await catalogo.GetDashboard(filtro);
                if (result.StatusCode == 400)
                    return BadRequest(ResponseClass.ErrorResponse(statusCode:400, message: result.Message, error: new Exception()));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
    }
}
