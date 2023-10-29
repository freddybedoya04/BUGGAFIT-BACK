using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class PERFILES
    {
        [Key]
        public string PER_CODIGO { get; set; }
        public string PER_NOMBRE { get; set; }
        public DateTime PER_TIMESPAN { get; set; }
        public bool PER_ESTADO { get; set; }
    }
}
