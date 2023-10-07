using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ICatalogoVentas catalgo;

        public VentasController(ICatalogoVentas catalgo)
        {
            this.catalgo = catalgo;
        }

        #region Metodos API
        // GET: api/Ventas
        [HttpGet("GetVentas")]
        public async Task<ActionResult<ResponseObject>> GetVentas()
        {
            try
            {
                var result = await catalgo.ListarVentasAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // GET: api/Ventas/5
        [HttpGet("GetVenta/{id}")]
        public async Task<ActionResult<ResponseObject>> GetVenta(int id)
        {
            try
            {
                var result = await catalgo.ListarVentaPorIDAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // POST: api/Ventas
        [HttpPost("PostVenta")]
        public async Task<ActionResult<ResponseObject>> PostVenta(Ventas ventas)
        {
            try
            {
                return Ok(await catalgo.CrearVentaAsync(ventas));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // PUT: api/Ventas/5
        [HttpPut("PutVentas/{id}")]
        public async Task<ActionResult<ResponseObject>> PutVentas(int id, Ventas ventas)
        {
            if (id != ventas.VEN_CODIGO)
                return BadRequest(ResponseClass.Response(statusCode: 400, message: "El id no coincide."));
            try
            {
                var result = await catalgo.ActualizarVentaAsync(ventas);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // DELETE: api/Ventas/5
        [HttpDelete("DeleteVentas/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteVentas(int id)
        {
            try
            {
                var result = await catalgo.BorrarVentaAsync(id);
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
