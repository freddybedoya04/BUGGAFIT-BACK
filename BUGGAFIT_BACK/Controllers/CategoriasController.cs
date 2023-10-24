using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICatalogoCategorias catalogo;

        public CategoriasController(ICatalogoCategorias catalgo)
        {
            this.catalogo = catalgo;
        }
        #region API
        // GET: api/GetCategorias
        [HttpGet("GetCategorias")]
        public async Task<ActionResult<ResponseObject>> GetCategorias()
        {
            try
            {
                var result = await catalogo.ListarCategoriasAsync();

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
