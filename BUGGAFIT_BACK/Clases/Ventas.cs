namespace BUGGAFIT_BACK.Clases
{
    public class Ventas
    {
        public int VEN_CODIGO { get; set; }
        public DateTime VEN_FECHACREACION { get; set; }
        public DateTime VEN_FECHAVENTA { get; set; }
        public string VEN_TIPOPAGO { get; set; }
        public int TIC_CODIGO { get; set; } // Clave foránea a TipoCuenta
        public string? TIC_NOMBRE { get; set; }
        public string CLI_ID { get; set; } // Clave foránea a Cliente
        public string? CLI_NOMBRE { get; set; }
        public string? CLI_DIRECCION { get; set; }
        public string? CLI_TIPOCLIENTE { get; set; }
        public string? CLI_UBICACION{ get; set; }
        public int? CLI_TELEFONO { get; set; }
        public string? CLI_CORREO {  get; set; }
        public float VEN_PRECIOTOTAL { get; set; }
        public bool VEN_ESTADOCREDITO { get; set; }
        public bool VEN_ENVIO { get; set; }
        public bool VEN_DOMICILIO { get; set; }
        public string? VEN_OBSERVACIONES { get; set; }
        public DateTime VEN_ACTUALIZACION { get; set; }
        public string USU_CEDULA { get; set; } // Clave foránea a Usuario
        public string? USU_NOMBRE { get; set; }
        public bool VEN_ESTADOVENTA { get; set; }
        public bool VEN_ESTADO { get; set; }
        public int? TIP_CODIGO { get; set; }
        public string? TIP_NOMBRE { get; set; }
        public bool? VEN_ESANULADA { get; set; }

        public ICollection<DetalleVenta>? DetalleVentas { get; set; }

    }
}
