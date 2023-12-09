using System.Drawing;

namespace BUGGAFIT_BACK.Clases
{
    public class EstadisticasGastos
    {
        public int MOG_CODIGO { get; set; }
        public string MOG_NOMBRE { get; set; }
        public double MOG_VALORGASTADO { get; set; }
    }
    public class EstadisticasGastosComparer : IEqualityComparer<EstadisticasGastos>
    {
        public bool Equals(EstadisticasGastos? item1, EstadisticasGastos? item2)
        {
            ArgumentNullException.ThrowIfNull(item1, nameof(item1));
            ArgumentNullException.ThrowIfNull(item2, nameof(item2));  
            return (item1.MOG_CODIGO == item2.MOG_CODIGO && item1.MOG_NOMBRE == item2.MOG_NOMBRE);
        }

        public int GetHashCode(EstadisticasGastos item)
        {
            int hCode = item.MOG_NOMBRE.Length;
            return hCode.GetHashCode();
        }
    }
}
