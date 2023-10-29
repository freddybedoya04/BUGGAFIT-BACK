using BUGGAFIT_BACK.Modelos;

namespace BUGGAFIT_BACK.Clases
{
    public class Gasto
    {
        public int GAS_CODIGO { get; set; }
        public DateTime GAS_FECHACREACION { get; set; }
        public DateTime GAS_FECHAGASTO { get; set; }
        public int MOG_CODIGO { get; set; }
        public float GAS_VALOR { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public string? TIC_NOMBRE { get; set; }
        public bool GAS_ESTADO { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario
        public string? USU_NOMBRE { get; set; }
        public bool GAS_PENDIENTE { get; set; }
        public int? VEN_CODIGO { get; set; } // Clave foránea a Ventas
        public string? MOG_NOMBRE { get; set; }

    }
}
