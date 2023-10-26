using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class TIPOSENVIOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TIP_CODIGO { get; set; }
        public string TIP_NOMBRE { get; set; }
        public float TIP_VALOR{ get; set; }
        public DateTime TIP_TIMESPAN{ get; set; }
        public bool TIP_ESTADO { get; set; }
    }
}
