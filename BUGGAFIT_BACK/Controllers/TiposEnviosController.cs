using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposEnviosController : ControllerBase
    {
        private readonly ICatalogoTiposEnvios catalogo;

        public TiposEnviosController(ICatalogoTiposEnvios catalogo)
        {
            this.catalogo = catalogo;
        }
        #region API
        // GET: api/GetMarcas
        [HttpGet("GetTiposEnvios")]
        public async Task<ActionResult<ResponseObject>> GetTiposEnvios()
        {
            try
            {
                var result = await catalogo.ListarTiposEnviosAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPost("PostTiposEnvios")]
        public async Task<ActionResult<ResponseObject>> PostTiposEnvios(TiposEnvios motivo)
        {
            try
            {
                return Ok(await catalogo.CrearTiposEnviosAsync(motivo));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPut("PutTipoEnvios/{id}")]
        public async Task<ActionResult<ResponseObject>> PutVentas(int id, TiposEnvios tiposEnvios)
        {
            if (id != tiposEnvios.TIP_CODIGO)
                return BadRequest(ResponseClass.Response(statusCode: 400, message: "El id no coincide."));
            try
            {
                var result = await catalogo.ActualizarTiposEnviosAsync(tiposEnvios);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return result;
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("DeleteTiposEnvios/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteTiposEnvios(int id)
        {
            try
            {
                var result = await catalogo.BorrarTiposEnviosAsync(id);
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
