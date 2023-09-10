using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class MOTIVOSGASTOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MOG_CODIGO { get; set; }
        public string MOG_NOMBRE { get; set; }
        public DateTime MOG_FECHACREACION { get; set; }
        public bool MOG_ESTADO { get; set; }
        public ICollection<GASTOS> Gastos { get; set; }
    }
}
