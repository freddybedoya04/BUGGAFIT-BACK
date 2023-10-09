using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly ICatalogoInventario catalogo;

        public InventarioController(ICatalogoInventario catalgo)
        {
            this.catalogo = catalgo;
        }

        #region Metodos API
        // GET: api/GetProducto
        [HttpGet("GetProductos")]
        public async Task<ActionResult<ResponseObject>> GetProductos()
        {
            try
            {
                var result = await catalogo.ListarProductosAsync();

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }
        // GET: api/GetProducto
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
        // GET: api/GetProducto
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

        // GET: api/GetProducto/5
        [HttpGet("GetProducto/{id}")]
        public async Task<ActionResult<ResponseObject>> GetProducto(string id)
        {
            try
            {
                var result = await catalogo.ListarProductoPorIDAsync(id);

                if (result.StatusCode == 204)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // POST: api/PostProducto
        [HttpPost("PostProducto")]
        public async Task<ActionResult<ResponseObject>> PostProducto(Producto producto)
        {
            try
            {
                return Ok(await catalogo.CrearProductoAsync(producto));
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // PUT: api/PutProducto/5
        [HttpPut("PutProducto/{id}")]
        public async Task<ActionResult<ResponseObject>> PutProducto(string id, Producto producto)
        {
            if (id != producto.PRO_CODIGO)
                return BadRequest(ResponseClass.Response(statusCode: 400, message: "El id no coincide."));
            try
            {
                var result = await catalogo.ActualizarProductoAsync(producto);
                if (result.StatusCode == 400)
                    return NotFound(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(statusCode: 500, title: $"Error al intentar procesar la peticion.", detail: $"{ex.Message} Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // DELETE: api/DeleteProducto/5
        [HttpDelete("DeleteProducto/{id}")]
        public async Task<ActionResult<ResponseObject>> DeleteProducto(int id)
        {
            try
            {
                var result = await catalogo.BorrarProductoAsync(id);
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
