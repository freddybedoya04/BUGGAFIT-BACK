using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class USUARIOS
    {
        [Key]
        public int USU_CEDULA { get; set; }
        public string USU_NOMBRE { get; set; }
        public string USU_CONTRASEÑA { get; set; }
        public string USU_ROL { get; set; }
        public DateTime USU_FECHACREACION { get; set; }
        public DateTime USU_FECHAACTUALIZACION { get; set; }
        public bool USU_ESTADO { get; set; }
    }
}
