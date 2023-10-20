
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Modelos
{
    public class VENTAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VEN_CODIGO { get; set; }
        public DateTime VEN_FECHACREACION { get; set; }
        public DateTime VEN_FECHAVENTA { get; set; }
        public string VEN_TIPOPAGO { get; set; }
        
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public string CLI_ID { get; set; } // Clave foránea a Cliente
        public float VEN_PRECIOTOTAL { get; set; }
        public bool? VEN_ESTADOCREDITO { get; set; } //Indica si la venta sera pagada a credito.
        public bool? VEN_ENVIO { get; set; }
        public bool? VEN_DOMICILIO { get; set; }
        public string VEN_OBSERVACIONES { get; set; }
        public DateTime? VEN_ACTUALIZACION { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario
        public bool VEN_ESTADOVENTA { get; set; } //Indica si la venta ya ha sido pagada y confirmada.
        public bool VEN_ESTADO { get; set; }
        public ICollection<DETALLEVENTAS> DETALLEVENTAS { get; set; }
        // Propiedades de navegación (relaciones)
        [ForeignKey("TIC_CODIGO")]
        public virtual TIPOSCUENTAS TIPOSCUENTAS { get; set; } // Relación con TipoCuenta
        [ForeignKey("CLI_ID")]
        public virtual CLIENTES CLIENTES { get; set; } // Relación con Cliente
        [ForeignKey("USU_CEDULA")]
        public virtual USUARIOS USUARIOS { get; set; } // Relación con Usuario
    }
}
