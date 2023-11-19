using BUGGAFIT_BACK.Modelos;

namespace BUGGAFIT_BACK.Clases
{
    public class Cartera
    {
        public int CAR_CODIGO { get; set; }
        public DateTime CAR_FECHACREACION { get; set; }
        public DateTime CAR_FECHACREDITO { get; set; }
        public DateTime CAR_FECHAACTUALIZACION { get; set; }
        public string? CAR_MOTIVO { get; set; }
        public int VEN_CODIGO { get; set; } // Clave foránea a Ventas
        public float? CAR_VALORCREDITO { get; set; }
        public float CAR_VALORABONADO { get; set; }
        public int? CAR_ESTADOCREDITO { get; set; }
        public bool CAR_ESTADO { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public string? TIC_NOMBRE { get; set; } // Clave foránea a TipoCuenta
        public bool? CAR_ESANULADA { get; set; }
        public string? USU_CEDULA { get; set; }

    }
}
