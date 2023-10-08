using BUGGAFIT_BACK.Modelos;

namespace BUGGAFIT_BACK.Clases
{
    public class DetalleVenta
    {
        public int VED_CODIGO { get; set; }
        public int VEN_CODIGO { get; set; } // Clave foránea a Ventas
        public string? PRO_CODIGO { get; set; } // Clave foránea a Producto
        public int VED_UNIDADES { get; set; }
        public float VED_PRECIOVENTA_UND { get; set; }
        public float VED_VALORDESCUENTO_UND { get; set; }
        public float VED_PRECIOVENTA_TOTAL { get; set; }
        public DateTime VED_ACTUALIZACION { get; set; }
        public bool VED_ESTADO { get; set; }

    }
}
