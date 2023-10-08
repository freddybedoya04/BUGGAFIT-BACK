using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.DTOs;
using Microsoft.AspNetCore.Mvc;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.AspNetCore.Authorization;
using BUGGAFIT_BACK.Clases;
using Microsoft.EntityFrameworkCore;
using BUGGAFIT_BACK.Security.interfaces;

namespace BUGGAFIT_BACK.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDBContext myDbContext;
        private IAuthenticationSystem _authentication;

        public LoginController(MyDBContext myDbContext, IAuthenticationSystem authentication)
        {
            this.myDbContext = myDbContext;
            _authentication = authentication;
        }

        #region Api Methods
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ResponseClass.GetModelStateErrors(ModelState));

                // search the user in db.
                //var query = await _postgresContext.Usercompanies.Where(x => x.Login == loginDTO.Username && x.State == true).ToListAsync();
                var query = await (from u in myDbContext.USUARIOS
                                   where u.USU_CEDULA == loginDTO.Username && u.USU_ESTADO == true
                                   select new Usuario
                                   {
                                       USU_CEDULA = u.USU_CEDULA,
                                       USU_NOMBRE = u.USU_NOMBRE,
                                       USU_CONTRASEÑA = u.USU_CONTRASEÑA,
                                       USU_ROL = u.USU_ROL,
                                       USU_ESTADO = u.USU_ESTADO,
                                   }).ToListAsync();

                if (query.Count <= 0)
                    return NotFound(ResponseClass.Response(404, "User not Found. "));

                var user = query.FirstOrDefault();
                if (user == null || user.USU_CONTRASEÑA == null)
                    return StatusCode(500, ResponseClass.ErrorResponse(500, "Error en validar el usuario.", new Exception()));
                if (loginDTO.Contraseña != user.USU_CONTRASEÑA)
                    return NotFound(ResponseClass.Response(404, "User not Found. "));

                string token = _authentication.GenerateJWTToken(user);
                _authentication.ValidateToken(token);

                return Ok(ResponseClass.Response(200, "Login successfull. ", user, token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseClass.ErrorResponse(500, $" {ex.Message}. ", ex));
            }
        }
        #endregion
    }
}
