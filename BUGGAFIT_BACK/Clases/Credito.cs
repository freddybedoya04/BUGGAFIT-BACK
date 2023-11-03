using BUGGAFIT_BACK.Modelos;

namespace BUGGAFIT_BACK.Clases
{
    public class Credito
    {
        public string CLI_ID { get; set; }
        public string CLI_NOMBRE { get; set; }
        public List<VENTAS> Ventas { get; set; }
        public List<Cartera> Carteras { get; set; }
        public float TotalVendido { get; set; }
        public float TotalAbonado { get; set; }
        public float DiferenciaTotal { get; set; }

    }
}
