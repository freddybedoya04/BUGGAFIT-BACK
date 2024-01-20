using BUGGAFIT_BACK.Clases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class DETALLEVENTAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VED_CODIGO { get; set; }
        public int VEN_CODIGO { get; set; } // Clave foránea a Ventas
        public string PRO_CODIGO { get; set; } // Clave foránea a Producto
        public int VED_UNIDADES { get; set; }
        public float VED_PRECIOVENTA_UND { get; set; }
        public float VED_VALORDESCUENTO_UND { get; set; }
        public float? PRO_PRECIO_COMPRA { get; set; }
        public float VED_PRECIOVENTA_TOTAL { get; set; }
        public DateTime VED_ACTUALIZACION { get; set; }
        public bool VED_ESTADO { get; set; }

        // Propiedades de navegación (relaciones)
        [ForeignKey("VEN_CODIGO")]
        public virtual VENTAS VENTAS { get; set; } // Relación con Ventas
        [ForeignKey("PRO_CODIGO")]
        public virtual PRODUCTOS PRODUCTOS { get; set; } // Relación con Producto
    }
}
