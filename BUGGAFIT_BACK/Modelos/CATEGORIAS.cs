using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class CATEGORIAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CAT_CODIGO { get; set; }
        public string CAT_NOMBRE { get; set; }
        public DateTime CAT_FECHACREACION { get; set; }
        public bool CAT_ESTADO { get; set; }
        public ICollection<PRODUCTOS> PRODUCTOS { get; set; }
    }
}
