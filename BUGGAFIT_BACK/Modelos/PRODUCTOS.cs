using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUGGAFIT_BACK.Modelos
{
    public class PRODUCTOS
    {
        [Key]
        public string PRO_CODIGO { get; set; }
        [Required]
        public string PRO_NOMBRE { get; set; }
        [Required]
        public int MAR_CODIGO { get; set; }
        [Required]
        public int CAT_CODIGO { get; set; }
        [Required]
        public float PRO_PRECIO_COMPRA { get; set; }
        public float PRO_PRECIOVENTA_DETAL { get; set; }
        public float PRO_PRECIOVENTA_MAYORISTA { get; set; }
        public int PRO_UNIDADES_DISPONIBLES { get; set; }
        public int PRO_UNIDADES_MINIMAS_ALERTA { get; set; }
        public DateTime PRO_ACTUALIZACION { get; set; }
        public DateTime PRO_FECHACREACION { get; set; }
        public bool PRO_ESTADO { get; set; }
        [ForeignKey("CAT_CODIGO")]
        public virtual CATEGORIAS CATEGORIA { get; set; }
        [ForeignKey("MAR_CODIGO")]
        public virtual MARCAS MARCA { get; set; }
        public bool? PRO_REGALO { get;set; }
        public int? PRO_UNIDADREGALO { get; set; }
        public int? PRO_UNIDAD_MINIMAREGALO { get; set; }
    }
}
