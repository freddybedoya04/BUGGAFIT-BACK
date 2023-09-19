using BUGGAFIT_BACK.Clases;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoGastos
    {
        Gasto ListarGastoPorID(int Id);
        List<Gasto> ListarGastos();
        Gasto CrearGasto(Gasto gasto);
        void ActualizarGasto(Gasto gasto);
        void BorrarGasto(int Id);

        // Async Methods
        Task<Gasto> CrearGastoAsync(Gasto gasto);
        Task<List<Gasto>> ListarGastosAsync();
        Task ActualizarGastoAsync(Gasto gasto);
        Task BorrarGastoAsync(int Id);
        Task<Gasto> ListarGastoPorIDAsync(int Id);
    }
}
