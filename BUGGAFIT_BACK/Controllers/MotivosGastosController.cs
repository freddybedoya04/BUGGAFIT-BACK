using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotivosGastosController : ControllerBase
    {
        private readonly ICatalogoMotivosGastos catalogo;

        public MotivosGastosController(ICatalogoMotivosGastos catalogo)
        {
            this.catalogo = catalogo;
        }
        #region API
        // GET: api/GetMarcas
        [HttpGet("GetMotivoGasto")]
        public async Task<ActionResult<ResponseObject>> GetMovitosGastos()
        {
            try
            {
                var result = await catalogo.ListarMotivoGastoAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPost("PostMotivoGasto")]
        public async Task<ActionResult<ResponseObject>> PostMotivoGasto(MotivoGasto motivo)
        {
            try
            {
                return Ok(await catalogo.CrearMotivoGastoAsync(motivo));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpPut("PutMotivoGasto")]
        public async Task<ActionResult<ResponseObject>> PutMotivoGasto(MotivoGasto motivo)
        {
            try
            {
                return Ok(await catalogo.ActualizarMotivoGastoAsync(motivo));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("DeleteMotivoGasto/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteMotivoGasto(int id)
        {
            try
            {
                var result = await catalogo.BorrarMotivoGastoAsync(id);
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
