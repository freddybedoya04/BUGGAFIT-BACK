using Microsoft.IdentityModel.Tokens;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BUGGAFIT_BACK.Security.interfaces;
using BUGGAFIT_BACK.Clases;

namespace BUGGAFIT_BACK.Security.Interfaces.Implementations
{
    public class AuthenticationSystem : IAuthenticationSystem
    {
        private readonly string _secretKey;
        private readonly string _audienceToken;
        private readonly string _issuerToken;
        private readonly string _expireTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="audienceToken"></param>
        /// <param name="issuerToken"></param>
        /// <param name="expireTime"></param>
        public AuthenticationSystem(string secretKey, string audienceToken, string issuerToken, string expireTime)
        {
            _secretKey = secretKey;
            _audienceToken = audienceToken;
            _issuerToken = issuerToken;
            _expireTime = expireTime;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretKey"></param>
        public AuthenticationSystem(string secretKey, string expireTime)
        {
            _secretKey = secretKey;
            _expireTime = expireTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GenerateJWTToken(Usuario userInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(_secretKey) || string.IsNullOrEmpty(_expireTime))
                    throw new ArgumentNullException("The secret Key or the Expire Time of the Token Generator have not been configured.");
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.USU_CEDULA.ToString()),
                new Claim(ClaimTypes.Role, userInfo.USU_ROL),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                var token = new JwtSecurityToken(
                issuer: _issuerToken,
                audience: _audienceToken,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception("Error. ", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserDTO ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
                
                // return user id from JWT token if validation successful
                return new UserDTO
                {
                    IdUser= int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value),
                    Idclient= int.Parse(jwtToken.Claims.First(x => x.Type == "client").Value),
                    Role= jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value,
                };
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw new SecurityTokenExpiredException(ex.Message);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

    }
}
