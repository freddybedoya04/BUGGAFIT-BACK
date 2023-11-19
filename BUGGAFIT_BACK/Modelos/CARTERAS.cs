using BUGGAFIT_BACK.Clases;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class CARTERAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CAR_CODIGO { get; set; }
        public DateTime CAR_FECHACREACION { get; set; }
        public DateTime CAR_FECHACREDITO { get; set; }
        public DateTime CAR_FECHAACTUALIZACION { get; set; }
        public string CAR_MOTIVO { get; set; }
        public int VEN_CODIGO { get; set; } // Clave foránea a Ventas
        public float CAR_VALORCREDITO { get; set; }
        public float CAR_VALORABONADO { get; set; }
        public int CAR_ESTADOCREDITO { get; set; }
        public bool CAR_ESTADO { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public bool? CAR_ESANULADA { get; set; }
        public string? USU_CEDULA { get; set; }
        // Propiedades de navegación (relaciones)
        [ForeignKey("VEN_CODIGO")]
        public VENTAS VENTA { get; set; } // Relación con Ventas
        [ForeignKey("TIC_CODIGO")]
        public TIPOSCUENTAS TIPOSCUENTAS { get; set; } // Relación con Ventas


    }
}
