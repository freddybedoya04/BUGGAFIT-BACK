namespace BUGGAFIT_BACK.Clases
{
    public class TipoCuenta
    {
        public int TIC_CODIGO { get; set; }
        public string TIC_NOMBRE { get; set; }
        public int TIC_NUMEROREFERENCIA { get; set; }
        public DateTime TIC_FECHACREACION { get; set; }
        public bool TIC_ESTADO { get; set; }
        public float? TIC_DINEROTOTAL { get; set; }
    }
}
