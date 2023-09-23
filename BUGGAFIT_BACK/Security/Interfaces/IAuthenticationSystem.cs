using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;

namespace BUGGAFIT_BACK.Security.interfaces
{
    public interface IAuthenticationSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateJWTToken(Usuario user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        UserDTO ValidateToken(string token);
    }
}
