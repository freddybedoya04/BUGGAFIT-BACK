namespace BUGGAFIT_BACK.Clases
{
    public class DetalleCompra
    {
        public int DEC_CODIGO { get; set; }
        public int COM_CODIGO { get; set; } // Clave foránea a Compras
        public string PRO_CODIGO { get; set; } // Clave foránea a Producto
        public string? PRO_NOMBRE { get; set; }
        public int DEC_UNIDADES { get; set; }
        public float DEC_PRECIOCOMPRA_PRODUCTO { get; set; }
        public float DEC_PRECIOTOTAL { get; set; }
        public DateTime DEC_FECHACREACION { get; set; }
        public DateTime DEC_FECHAACTUALIZACION { get; set; }
        public bool DEC_ESTADO { get; set; }

    }

}
