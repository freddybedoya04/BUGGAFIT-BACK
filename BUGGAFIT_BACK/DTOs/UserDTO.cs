using System.Collections.Generic;

namespace BUGGAFIT_BACK.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public int Idclient { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
