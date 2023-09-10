using BUGGAFIT_BACK.Clases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class DETALLECOMPRAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DEC_CODIGO { get; set; }
        public int COM_CODIGO { get; set; } // Clave foránea a Compras
        public string PRO_CODIGO { get; set; } // Clave foránea a Producto
        public int DEC_UNIDADES { get; set; }
        public float DEC_PRECIOCOMPRA_PRODUCTO { get; set; }
        public float DEC_PRECIOTOTAL { get; set; }
        public DateTime DEC_FECHACREACION { get; set; }
        public DateTime DEC_FECHAACTUALIZACION { get; set; }
        public bool DEC_ESTADO { get; set; }

        // Propiedades de navegación (relaciones)
        [ForeignKey("COM_CODIGO")]
        public COMPRAS COMRPA { get; set; } // Relación con Compras
        [ForeignKey("PRO_CODIGO")]
        public PRODUCTOS PRODUCTO { get; set; } // Relación con Producto
    }
}
