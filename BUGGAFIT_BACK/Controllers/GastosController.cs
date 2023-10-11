using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly ICatalogoGastos catalgo;

        public GastosController(ICatalogoGastos catalgo)
        {
            this.catalgo = catalgo;
        }

        #region Metodos API
        // GET: api/GetGasto
        [HttpGet("GetGasto")]
        public async Task<ActionResult<ResponseObject>> GetGastos()
        {
            try
            {
                var result = await catalgo.ListarGastosAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // GET: api/GetGasto/5
        [HttpGet("GetGasto/{id}")]
        public async Task<ActionResult<ResponseObject>> GetGasto(int id)
        {
            try
            {
                var result = await catalgo.ListarGastoPorIDAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // GET: api/GetGasto/5
        [HttpGet("GetGasto/MotivoEnvio")]
        public async Task<ActionResult<ResponseObject>> GetMotivoGastosDeEnvio()
        {
            try
            {
                var result = await catalgo.ListarMotivoGastosDeEnvioAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // POST: api/PostGasto
        [HttpPost("PostGasto")]
        public async Task<ActionResult<ResponseObject>> PostGasto(Gasto gasto)
        {
            try
            {
                return Ok(await catalgo.CrearGastoAsync(gasto));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // PUT: api/PutGasto/5
        [HttpPut("PutGasto/{id}")]
        public async Task<ActionResult<ResponseObject>> PutGasto(int id, Gasto gasto)
        {
            if (id != gasto.GAS_CODIGO)
                return BadRequest(ResponseClass.Response(statusCode: 400, message: "El id no coincide."));
            try
            {
                var result = await catalgo.ActualizarGastoAsync(gasto);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // DELETE: api/DeleteGasto/5
        [HttpDelete("DeleteGasto/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteGasto(int id)
        {
            try
            {
                var result = await catalgo.BorrarGastoAsync(id);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        #endregion
    }
}
