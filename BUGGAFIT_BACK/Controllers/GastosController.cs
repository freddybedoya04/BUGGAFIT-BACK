using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly ICatalogoGastos catalogo;

        public GastosController(ICatalogoGastos catalgo)
        {
            this.catalogo = catalgo;
        }

        #region Metodos API
        // GET: api/GetGasto
        [HttpGet("GetGastos")]
        public async Task<ActionResult<ResponseObject>> GetGastos()
        {
            try
            {
                var result = await catalogo.ListarGastosAsync();

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
                var result = await catalogo.ListarGastoPorIDAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // POST api/<ComprasController>
        [HttpPost]
        [Route("ListarGastosPorFecha")]
        public async Task<IActionResult> ListarGastosPorFecha([FromBody] FiltrosDTO filtro)
        {
            try
            {
                var result = await catalogo.ListarGastosPorFecha(filtro);
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
                var result = await catalogo.ListarMotivoGastosDeEnvioAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpGet("GetCerrarGasto/{id}")]
        public async Task<ActionResult<ResponseObject>> GetCerrarGasto(int id)
        {
            try
            {
                var result = await catalogo.CerrarGasto(id);
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
                return Ok(await catalogo.CrearGastoAsync(gasto));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        // POST: api/PostGasto
        [HttpPost("PostGastoVenta")]
        public async Task<ActionResult<ResponseObject>> PostGastoVenta(Gasto gasto)
        {
            try
            {
                return Ok(await catalogo.CrearGastoVentaAsync(gasto));
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
                var result = await catalogo.ActualizarGastoAsync(gasto);
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
                var result = await catalogo.BorrarGastoAsync(id);
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
