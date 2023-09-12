namespace BUGGAFIT_BACK.DTOs.Response
{
    /// <summary>
    /// class to Return a Login response Object with the most relevant information
    /// </summary>
    public class LoginResponseDTO
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public int Idclient { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
        public double GTM { get; set; } 
    }
}
