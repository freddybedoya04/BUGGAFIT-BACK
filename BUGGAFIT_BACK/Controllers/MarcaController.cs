using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {

        private readonly ICatalogoMarcas catalogo;

        public MarcaController(ICatalogoMarcas catalogo)
        {
            this.catalogo = catalogo;
        }
        #region API
        // GET: api/GetMarcas
        [HttpGet("GetMarcas")]
        public async Task<ActionResult<ResponseObject>> GetMarcas()
        {
            try
            {
                var result = await catalogo.ListarMarcasAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPost("PostMarca")]
        public async Task<ActionResult<ResponseObject>> PostMarca(Marca marca)
        {
            try
            {
                return Ok(await catalogo.CrearMarcaAsync(marca));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("DeleteMarca/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteAbono(int id)
        {
            try
            {
                var result = await catalogo.BorrarMarcaAsync(id);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return result;
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        #endregion

    }
}
