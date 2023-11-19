﻿namespace BUGGAFIT_BACK.Clases
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
        public int TIC_CODIGO { get; set; }

    }

    public class TiposTransacciones
    {
        private TiposTransacciones(string value, bool esRetiroDeDinero) { Valor = value; EsRetiroDeDinero = esRetiroDeDinero; }
        private TiposTransacciones(string value) { Valor = value; EsRetiroDeDinero = false; }
        public string Valor { get; private set; }
        public bool EsRetiroDeDinero { get; private set; }
        public static TiposTransacciones VENTA { get { return new TiposTransacciones("VENTA", false); } }
        public static TiposTransacciones COMPRA { get { return new TiposTransacciones("COMPRA", true); } }
        public static TiposTransacciones GASTO { get { return new TiposTransacciones("GASTO", true); } }
        public static TiposTransacciones ABONO { get { return new TiposTransacciones("ABONO", false); } }
        public static TiposTransacciones TRANSFERENCIA { get { return new TiposTransacciones("TRANSFERENCIA", false); } }
        public static TiposTransacciones DEVOLUCION_VENTA { get { return new TiposTransacciones("DEVOLUCION_VENTA", true); } }
        public static TiposTransacciones DEVOLUCION_COMPRA { get { return new TiposTransacciones("DEVOLUCION_COMPRA", false); } }
        public static TiposTransacciones DEVOLUCION_GASTO { get { return new TiposTransacciones("DEVOLUCION_GASTO", false); } }
        public static TiposTransacciones DEVOLUCION_ABONO { get { return new TiposTransacciones("DEVOLUCION_ABONO", true); } }
        public static TiposTransacciones DEVOLUCION_TRANSFERENCIA { get { return new TiposTransacciones("DEVOLUCION_TRANSFERENCIA", true); } }
        public static TiposTransacciones GetTipoTransaccion(string tipoTransaccion)
        {
            return tipoTransaccion switch
            {
                "VENTA" => VENTA,
                "GASTO" => GASTO,
                "ABONO" => ABONO,
                "COMPRA" => COMPRA,
                "TRANSFERENCIA" => TRANSFERENCIA,
                "DEVOLUCION_ABONO" => DEVOLUCION_ABONO,
                "DEVOLUCION_COMPRA" => DEVOLUCION_COMPRA,
                "DEVOLUCION_GASTO" => DEVOLUCION_GASTO,
                "DEVOLUCION_TRANSFERENCIA" => DEVOLUCION_TRANSFERENCIA,
                "DEVOLUCION_VENTA" => DEVOLUCION_VENTA,
                _ => TRANSFERENCIA,
            };
        }
        public static TiposTransacciones GetTipoDevolucion(string tipoTransaccion)
        {
            return tipoTransaccion switch
            {
                "VENTA" => DEVOLUCION_VENTA,
                "GASTO" => DEVOLUCION_GASTO,
                "ABONO" => DEVOLUCION_ABONO,
                "COMPRA" => DEVOLUCION_COMPRA,
                "TRANSFERENCIA" => DEVOLUCION_TRANSFERENCIA,
                "DEVOLUCION_ABONO" => DEVOLUCION_ABONO,
                "DEVOLUCION_COMPRA" => DEVOLUCION_COMPRA,
                "DEVOLUCION_GASTO" => DEVOLUCION_GASTO,
                "DEVOLUCION_TRANSFERENCIA" => DEVOLUCION_TRANSFERENCIA,
                "DEVOLUCION_VENTA" => DEVOLUCION_VENTA,
                _ => DEVOLUCION_TRANSFERENCIA,
            };
        }
    }
}
