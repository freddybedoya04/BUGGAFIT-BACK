using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.DTOs
{
    public class LoginDTO
    {
        [Required (ErrorMessage = "The User is obligatory")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Password is obligatory")]
        public string Contraseña { get; set; }

        //[Required(ErrorMessage = "The Key is obligatory")]
        //public string Key { get; set; }
    }
}
