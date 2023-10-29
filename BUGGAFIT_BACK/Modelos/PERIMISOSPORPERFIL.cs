using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUGGAFIT_BACK.Modelos
{
    public class PERIMISOSPORPERFIL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PPP_CODIGO { get; set; }
        public string PAN_CODIGO { get; set; }
        public string PER_CODIGO { get; set; }
        public bool PPP_VER { get; set; }
        public bool PPP_AGREGAR { get; set; }
        public bool PPP_EDITAR { get; set; }
        public bool PPP_ELIMINAR { get; set; }
        [ForeignKey("PAN_CODIGO")]
        public virtual PANTALLAS PANTALLAS { get; set; } // Relación con TipoCuenta
        [ForeignKey("PER_CODIGO")]
        public virtual PERFILES PERFILES { get; set; } // Relación con Cliente
    }
}
