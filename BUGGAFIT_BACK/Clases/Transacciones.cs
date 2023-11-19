namespace BUGGAFIT_BACK.Clases
{
    public class Transacciones
    {
        public int TRA_CODIGO { get; set; } // AUTOINCREMENTAL
        public int TIC_CUENTA { get; set; }
        public string TRA_TIPO { get; set; }
        public DateTime TRA_FECHACREACION { get; set; }
        public bool TRA_CONFIRMADA { get; set; }
        public bool TRA_ESTADO { get; set; }
        public DateTime? TRA_FECHACONFIRMACION { get; set; }
        public string? TRA_CODIGOENLACE { get; set; }
        public bool TRA_FUEANULADA { get; set; }
        public int? TRA_NUMEROTRANSACCIONBANCO { get; set; }
        public string? USU_CEDULA_CONFIRMADOR { get; set; }
        public float? TRA_VALOR { get; set; }
    }

    public enum TiposTransacciones
    {
        //TODO: hacer los tipos de transacciones
    }
}
