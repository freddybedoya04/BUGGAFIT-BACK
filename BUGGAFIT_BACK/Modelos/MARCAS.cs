using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class MARCAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MAR_CODIGO { get; set; }
        public string MAR_NOMBRE { get; set; }
        public DateTime MAR_FECHACREACION { get; set; }
        public bool MAR_ESTADO { get; set; }

        public ICollection<PRODUCTOS> PRODUCTOS { get; set; }
    }
}
