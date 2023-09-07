namespace BUGGAFIT_BACK.Clases
{
    public class Producto
    {
        public string? PRO_CODIGO { get; set; }
        public string? PRO_NOMBRE { get; set; }
        public string? PRO_MARCA { get; set; }
        public string? PRO_CATEGORIA { get; set; }
        public float PRO_PRECIO_COMPRA { get; set; }
        public float PRO_PRECIOVENTA_DETAL { get; set; }
        public float PRO_PRECIOVENTA_MAYORISTA { get; set; }
        public int PRO_UNIDADES_DISPONIBLES { get; set; }
        public int PRO_UNIDADES_MINIMAS_ALERTA { get; set; }
        public DateTime PRO_ACTUALIZACION { get; set; }
        public DateTime PRO_FECHACREACION { get; set; }
        public bool PRO_ESTADO { get; set; }
    }
}
