using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class PRODUCTOS
    {
        [Key]
        public string PRO_CODIGO { get; set; }
        [Required]
        public string PRO_NOMBRE { get; set; }
        [Required]
        public string PRO_MARCA { get; set; }
        [Required]
        public string PRO_CATEGORIA { get; set; }
        [Required]
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
