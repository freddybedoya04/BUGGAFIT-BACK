using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
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
        // POST: api/Ventas
        [HttpPost("PostListadoVenta")]
        public async Task<ActionResult<ResponseObject>> PostListadoVenta(FiltrosDTO filtro)
        {
            try
            {
                return Ok(await catalgo.ListarVentasPorFechaAsync(filtro));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        // POST: api/Ventas
        [HttpPost("PostDetalleVenta")]
        public async Task<ActionResult<ResponseObject>> PostDetalleVenta(List<DetalleVenta> detallesVentas)
        {
            try
            {
                return Ok(await catalgo.CrearDetalleVentaAsync(detallesVentas));
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
        [HttpGet("ActualizarEstadoVenta/{id}")]
        public async Task<ActionResult<ResponseObject>> ActualizarEstadoVenta(int id)
        {
            try
            {
                var result = await catalgo.ActualizarEstadoVentaAsync(id);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpGet("FinalizarCredito/{id}")]
        public async Task<ActionResult<ResponseObject>> FinalizarCredito(int id)
        {
            try
            {
                var result = await catalgo.FinalizarCreditoAsync(id);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return result;
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpGet("ListarDetallePorCodigoVenta/{id}")]
        public async Task<ActionResult<ResponseObject>> ListarDetallePorCodigoVenta(int id)
        {
            try
            {
                return Ok(await catalgo.ListarDetallePorCodigoVentaAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("ListarAbonosPorCodigoVenta/{id}")]
        public async Task<ActionResult<ResponseObject>> ListarAbonosPorCodigoVenta(int id)
        {
            try
            {
                return Ok(await catalgo.ListarAbonosPorCodigoVentaAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPost("CrearAbono")]
        public async Task<ActionResult<ResponseObject>> CrearAbono(Cartera cartera)
        {
            try
            {
                return Ok(await catalgo.CrearAbonoAsync(cartera));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        [HttpDelete("DeleteAbono/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteAbono(int id)
        {
            try
            {
                var result = await catalgo.BorrarAbonoAsync(id);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return result;
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        [HttpPut("PutAbonos")]
        public async Task<ActionResult<ResponseObject>> PutAbono( Cartera cartera)
        {
            try
            {
                var result = await catalgo.ActualizarAbonoAsync(cartera);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return result;
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
