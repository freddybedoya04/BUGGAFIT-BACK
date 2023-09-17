using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUGGAFIT_BACK.Modelos
{
    public class CLIENTES
    {
        [Key]
        
        public string CLI_ID { get; set; }
        public string CLI_NOMBRE { get; set; }
        public string CLI_TIPOCLIENTE { get; set; }
        public string CLI_UBICACION { get; set; }
        public string CLI_DIRECCION { get; set; }
        public DateTime CLI_FECHACREACION { get; set; }
        public bool CLI_ESTADO { get; set; }
    }
}
