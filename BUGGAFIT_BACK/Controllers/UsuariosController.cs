using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ICatalogoUsuarios catalogoUsuarios;
        public UsuariosController(ICatalogoUsuarios context)
        {
            catalogoUsuarios = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                var usuarios = catalogoUsuarios.ListarUsuarios();
                return Ok(usuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
