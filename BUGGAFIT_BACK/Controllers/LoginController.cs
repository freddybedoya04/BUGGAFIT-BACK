using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.DTOs;
using Microsoft.AspNetCore.Mvc;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.AspNetCore.Authorization;
using BUGGAFIT_BACK.Clases;
using Microsoft.EntityFrameworkCore;
using BUGGAFIT_BACK.Security.interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BUGGAFIT_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                                   where u.USU_CEDULA == loginDTO.Cedula && u.USU_ESTADO == true
                                   select new Usuario
                                   {
                                       USU_CEDULA = u.USU_CEDULA,
                                       USU_NOMBRE = u.USU_NOMBRE,
                                       USU_CONTRASEÑA = u.USU_CONTRASEÑA,
                                       USU_ROL = u.USU_ROL,
                                       USU_ESTADO = u.USU_ESTADO,
                                   }).ToListAsync();

                if (query.Count <= 0)
                    return NotFound(ResponseClass.Response(404, "Error en validar el usuario. "));

                var user = query.FirstOrDefault();
                if (user == null || user.USU_CONTRASEÑA == null)
                    return NotFound(ResponseClass.ErrorResponse(404, "Error en validar el usuario.", new Exception()));
                if (EncriptarContraseña(loginDTO.Password) != user.USU_CONTRASEÑA)
                    return NotFound(ResponseClass.Response(404, "Error en validar el usuario. "));

                string token = _authentication.GenerateJWTToken(user);
                _authentication.ValidateToken(token);

                user.USU_CONTRASEÑA = "****";
                return Ok(ResponseClass.Response(200, "Login successfull. ", user, token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseClass.ErrorResponse(500, $" {ex.Message}. ", ex));
            }
        }

        private static string EncriptarContraseña(string contraseña)
        {
            byte[] salt = Convert.FromBase64String("CGYzqeN4plZekNC88Umm1Q=="); // divide by 8 to convert bits to bytes
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: contraseña!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        #endregion
    }
}
