namespace BUGGAFIT_BACK.Clases
{
    public class Ventas
    {
        public int VEN_CODIGO { get; set; }
        public DateTime VEN_FECHACREACION { get; set; }
        public DateTime VEN_FECHAVENTA { get; set; }
        public string VEN_TIPOPAGO { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public string CLI_ID { get; set; } // Clave foránea a Cliente
        public float VEN_PRECIOTOTAL { get; set; }
        public bool VEN_ESTADOCREDITO { get; set; }
        public bool VEN_ENVIO { get; set; }
        public bool VEN_DOMICILIO { get; set; }
        public string VEN_OBSERVACIONES { get; set; }
        public DateTime VEN_ACTUALIZACION { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario
        public bool VEN_ESTADOVENTA { get; set; }
        public bool VEN_ESTADO { get; set; }
        public ICollection<DetalleVenta> detalleVentas { get; set; }

    }
}
