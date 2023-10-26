using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.DTOs
{
    public class LoginDTO
    {
        [Required (ErrorMessage = "The cedula is obligatory")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "The Password is obligatory")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "The Key is obligatory")]
        //public string Key { get; set; }
    }
}
