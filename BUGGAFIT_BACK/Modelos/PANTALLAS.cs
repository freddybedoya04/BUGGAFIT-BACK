using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class PANTALLAS
    {
        [Key]
        public string PAN_CODIGO { get; set; }
        public string PAN_NOMBRE { get; set; }
        public string PAN_PATH { get; set; }
        public string PAN_ICON { get; set; }
        public string PAN_TEXT { get; set; }
        public DateTime PAN_TIMESPAN { get; set; }
        public bool PAN_ESTADO { get; set; }
    }
}
