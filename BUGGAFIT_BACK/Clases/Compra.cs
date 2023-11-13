namespace BUGGAFIT_BACK.Clases
{
    public class Compra
    {
        public int COM_CODIGO { get; set; }
        public DateTime COM_FECHACREACION { get; set; }
        public DateTime COM_FECHACOMPRA { get; set; }
        public float COM_VALORCOMPRA { get; set; }
        public string COM_PROVEEDOR { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public string? TIC_NOMBRE { get; set; }
        public DateTime COM_FECHAACTUALIZACION { get; set; }
        public bool COM_ENBODEGA { get; set; }
        public bool COM_ESTADO { get; set; }
        public bool COM_CREDITO { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario
        public bool? COM_ESANULADA { get; set; }
        public List<DetalleCompra> DetalleCompras { get; set; }

    }
}
