using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Clases;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
        [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ICatalogoClientes catalgo;

        public ClientesController(ICatalogoClientes catalgo)
        {
            this.catalgo = catalgo;
        }

        #region Metodos API
        // GET: api/Clientes
        [HttpGet("GetClientes")]
        public async Task<ActionResult<ResponseObject>> GetClientes()
        {
            try
            {
                var result = await catalgo.ListarClientesAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // GET: api/Cliente/5
        [HttpGet("GetCliente/{id}")]
        public async Task<ActionResult<ResponseObject>> GetCliente(string id)
        {
            try
            {
                var result = await catalgo.ListarClientePorIDAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // POST: api/Clientes
        [HttpPost("PostCliente")]
        public async Task<ActionResult<ResponseObject>> PostCliente(Cliente ventas)
        {
            try
            {
                return Ok(await catalgo.CrearClienteAsync(ventas));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // PUT: api/Clientes/5
        [HttpPut("PutCliente/{id}")]
        public async Task<ActionResult<ResponseObject>> PutClientes(string id, Cliente cliente)
        {
            if (id != cliente.CLI_ID)
                return BadRequest(ResponseClass.Response(statusCode: 400, message: "El id no coincide."));
            try
            {
                var result = await catalgo.ActualizarClienteAsync(cliente);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("DeleteCliente/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteClientes(string id)
        {
            try
            {
                var result = await catalgo.BorrarClienteAsync(id);
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
