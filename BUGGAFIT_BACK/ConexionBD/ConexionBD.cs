using BUGGAFIT_BACK.Modelos.Entidad;

namespace BUGGAFIT_BACK.ConexionBD
{
    public class ConexionBD
    {
        private static MyDBContext _instance;

        // Este método te permitirá establecer la instancia de MyDBContext desde Program.cs
        public static void SetDbContext(MyDBContext dbContext)
        {
            _instance = dbContext;
        }

        public static MyDBContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("El DbContext no ha sido configurado. Asegúrate de llamar a SetDbContext desde Program.cs.");
                }
                return _instance;
            }
        }
    }
}
