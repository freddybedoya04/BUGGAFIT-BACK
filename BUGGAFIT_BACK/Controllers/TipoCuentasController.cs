using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCuentasController : Controller
    {
        private readonly ICatalogoTipoCuenta catalogo;

        public TipoCuentasController(ICatalogoTipoCuenta catalogo)
        {
            this.catalogo = catalogo;
        }

        #region Metodos API
        // GET: api/GetTipoCuenta
        [HttpGet("GetTipoCuentas")]
        public async Task<ActionResult<ResponseObject>> GetTipoCuentas()
        {
            try
            {
                var result = await catalogo.ListarTipoCuentasAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // GET: api/GetTipoCuenta/5
        [HttpGet("GetTipoCuenta/{id}")]
        public async Task<ActionResult<ResponseObject>> GetTipoCuenta(int id)
        {
            try
            {
                var result = await catalogo.ListarTipoCuentaPorIDAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // POST: api/PostTipoCuenta
        [HttpPost("PostTipoCuenta")]
        public async Task<ActionResult<ResponseObject>> PostTipoCuenta(TipoCuenta tipoCuenta)
        {
            try
            {
                return Ok(await catalogo.CrearTipoCuentaAsync(tipoCuenta));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // PUT: api/PutTipoCuenta/5
        [HttpPut("PutTipoCuenta/{id}")]
        public async Task<ActionResult<ResponseObject>> PutTipoCuenta(int id, TipoCuenta cuenta)
        {
            if (id != cuenta.TIC_CODIGO)
                return BadRequest(ResponseClass.Response(statusCode: 400, message: "El id no coincide."));
            try
            {
                var result = await catalogo.ActualizarTipoCuentaAsync(cuenta);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // DELETE: api/DeleteTipoCuenta/5
        [HttpDelete("DeleteTipoCuenta/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteTipoCuenta(int id)
        {
            try
            {
                var result = await catalogo.BorrarTipoCuentaAsync(id);
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
