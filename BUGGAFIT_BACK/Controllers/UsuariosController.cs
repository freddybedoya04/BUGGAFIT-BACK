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
        private readonly MyDBContext myDBContext;
        public UsuariosController(MyDBContext context)
        {
            myDBContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = new CatalogoUsuario(myDBContext).ListarUsuarios();
            return Ok(usuarios);
        }
    }
}
