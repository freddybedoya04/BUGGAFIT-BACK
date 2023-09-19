using BUGGAFIT_BACK.Catalogos;
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
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
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

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
