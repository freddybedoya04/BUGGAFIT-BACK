namespace BUGGAFIT_BACK.Clases
{
    public class EstadisticasGastosComparerBase
    {
        public bool Equals(EstadisticasGastos x, EstadisticasGastos y)
        {
            ArgumentNullException.ThrowIfNull(x, "x");
            ArgumentNullException.ThrowIfNull(y, "y");
            return (x.MOG_CODIGO == y.MOG_CODIGO && x.MOG_NOMBRE == y.MOG_NOMBRE);
        }
    }
}