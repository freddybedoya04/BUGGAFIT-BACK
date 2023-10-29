using BUGGAFIT_BACK.Catalogos;
using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            try
            {
                var usuarios = await catalogoUsuarios.ListarUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("PostUsuario")]
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioAgregado = await catalogoUsuarios.AgregarUsuarioAsync(usuario);
                return CreatedAtAction("ListarUsuarios", usuarioAgregado);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("PutUsuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                await catalogoUsuarios.ActualizarUsuarioAsync(usuario);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("BuscarUsuarioPorCedula/{cedula}")]
        public async Task<IActionResult> BuscarUsuarioPorCedula(string cedula)
        {
            try
            {
                var usuario = await catalogoUsuarios.BuscarUsuarioPorCedulaAsync(cedula);

                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound(); // Usuario no encontrado
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("ListarPantallasPermisos/{perfil}")]
        public async Task<IActionResult> ListarPantallasPermisos(string perfil)
        {
            try
            {
                var usuario = await catalogoUsuarios.ListarPantallasPermisosAsync(perfil);
                    return Ok(usuario);


            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> BorrarUsuario(string cedula)
        {
            try
            {
                await catalogoUsuarios.BorrarUsuarioAsync(cedula);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("ValidarUsuarioAdmin")]
        public async Task<IActionResult> ValidarUsuarioAdmin(LoginDTO loginDTO)
        {
            try
            {
                return Ok(await catalogoUsuarios.ValidadUsuarioConPermisosAdmin(loginDTO));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        [HttpGet("GetPerfiles")]
        public async Task<IActionResult> ListarPerfiles()
        {
            try
            {
                var perfiles = await catalogoUsuarios.ListarPerfilesAsync();
                return Ok(perfiles);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
