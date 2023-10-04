using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        public readonly ICatalogoCompras catalogoCompras;
        public ComprasController(ICatalogoCompras context)
        {
            catalogoCompras = context;
        }
        // GET: api/<ComprasController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ComprasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarCompraPorID(int id)
        {
            try
            {
                var compras = catalogoCompras.BuscarCompraPorID(id);
                return Ok(compras);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<ComprasController>
        [HttpPost]
        [Route("ListarComprasPorFecha")]
        public async Task<IActionResult> ListarComprasPorFecha([FromBody] FiltrosDTO filtro)
        {
            try
            {
                var compras=catalogoCompras.ListarComprasPorFecha(filtro);
                return Ok(compras);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // POST api/<ComprasController>
        [HttpPost]
        [Route("CrearCompra")]
        public async Task<IActionResult> CrearCompra([FromBody] Compra nuevacompra)
        {
            try
            {
                catalogoCompras.CrearCompra(nuevacompra);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPut]
        [Route("ActualizarCompra")]
        public async Task<IActionResult> ActualizarCompra([FromBody] Compra nuevacompra)
        {
            try
            {
                catalogoCompras.ActualizarCompra(nuevacompra);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // PUT api/<ComprasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Compra compra)
        {
        }

        // DELETE api/<ComprasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                catalogoCompras.EliminarCompra(id);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
