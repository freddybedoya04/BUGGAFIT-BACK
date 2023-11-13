using BUGGAFIT_BACK.Clases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUGGAFIT_BACK.Modelos
{
    public class GASTOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GAS_CODIGO { get; set; }
        public DateTime GAS_FECHACREACION { get; set; }
        public DateTime GAS_FECHAGASTO { get; set; }
        public int MOG_CODIGO{ get; set; }
        public float GAS_VALOR { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public bool GAS_ESTADO { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario
        public bool GAS_PENDIENTE { get; set; }
        public int? VEN_CODIGO { get; set; } // Clave foránea a Ventas
        public string? GAS_OBSERVACIONES { get; set; }
        public bool? GAS_ESANULADA { get; set; }
        // Propiedades de navegación (relaciones)
        [ForeignKey("TIC_CODIGO")]
        public TIPOSCUENTAS TipoCuentas { get; set; } // Relación con TipoCuenta
        [ForeignKey("USU_CEDULA")]
        public USUARIOS Usuarios { get; set; } // Relación con Usuario
        [ForeignKey("VEN_CODIGO")]
        public VENTAS? Ventas { get; set; } // Relación con Ventas
        [ForeignKey("MOG_CODIGO")]
        public MOTIVOSGASTOS MOTIVOSGASTOS { get; set; }

    }
}
