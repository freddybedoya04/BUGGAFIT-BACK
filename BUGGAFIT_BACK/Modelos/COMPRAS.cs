using BUGGAFIT_BACK.Clases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUGGAFIT_BACK.Modelos
{
    public class COMPRAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int COM_CODIGO { get; set; }
        public DateTime COM_FECHACREACION { get; set; }
        public DateTime COM_FECHACOMPRA { get; set; }
        public float COM_VALORCOMPRA { get; set; }
        public string COM_PROVEEDOR { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public DateTime COM_FECHAACTUALIZACION { get; set; }
        public bool COM_ENBODEGA { get; set; }
        public bool COM_ESTADO { get; set; }
        public bool COM_CREDITO { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario

        public ICollection<DETALLECOMPRAS>DetalleCompras { get; set; }    
        // Propiedades de navegación (relaciones)
        [ForeignKey("TIC_CODIGO")]
        public virtual TIPOSCUENTAS TipoCuenta { get; set; } // Relación con TipoCuenta
        [ForeignKey("USU_CEDULA")]
        public virtual USUARIOS Usuario { get; set; } // Relación con Usuario
    }
}
