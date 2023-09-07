using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Modelos.Entidad
{
    public class MyDBContext:DbContext
    {
        public DbSet<CLIENTES> CLIENTES { get; set; }
        public DbSet<DETALLEVENTAS> DETALLEVENTAS { get; set; }
        public DbSet<PRODUCTOS> PRODUCTOS { get; set; }
        public DbSet<TIPOSCUENTAS> TIPOSCUENTAS { get; set; }
        public DbSet<USUARIOS> USUARIOS { get; set; }
        public DbSet<VENTAS> VENTAS { get; set; }

        public MyDBContext(DbContextOptions <MyDBContext>options):base(options) { }    
    }
}
