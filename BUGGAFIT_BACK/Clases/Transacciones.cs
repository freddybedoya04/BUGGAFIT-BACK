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
        public int TIC_CODIGO { get; set; }

    }

    public class TiposTransacciones
    {
        private TiposTransacciones(string value) { Valor = value; }
        public string Valor { get; private set; }
        public static TiposTransacciones VENTA { get { return new TiposTransacciones("VENTA"); } }
        public static TiposTransacciones COMPRA { get { return new TiposTransacciones("COMPRA"); } }
        public static TiposTransacciones GASTO { get { return new TiposTransacciones("GASTO"); } }
        public static TiposTransacciones ABONO { get { return new TiposTransacciones("ABONO"); } }
        public static TiposTransacciones TRANSFERENCIA { get { return new TiposTransacciones("TRANSFERENCIA"); } }
        public static TiposTransacciones DEVOLUCION_VENTA { get { return new TiposTransacciones("DEVOLUCION_VENTA"); } }
        public static TiposTransacciones DEVOLUCION_COMPRA { get { return new TiposTransacciones("DEVOLUCION_COMPRA"); } }
        public static TiposTransacciones DEVOLUCION_GASTO { get { return new TiposTransacciones("DEVOLUCION_GASTO"); } }
        public static TiposTransacciones DEVOLUCION_ABONO { get { return new TiposTransacciones("DEVOLUCION_ABONO"); } }
        public static TiposTransacciones DEVOLUCION_TRANSFERENCIA { get { return new TiposTransacciones("DEVOLUCION_TRANSFERENCIA"); } }
    }
}
