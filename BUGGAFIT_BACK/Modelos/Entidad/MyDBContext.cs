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
        public DbSet<COMPRAS> COMPRAS { get; set; }
        public DbSet<DETALLECOMPRAS> DETALLECOMPRAS { get; set; }
        public DbSet<GASTOS> GASTOS { get; set; }
        public DbSet<MOTIVOSGASTOS> MOTIVOSGASTOS { get; set; }
        public DbSet<CARTERAS> CARTERAS { get; set; }
        public DbSet<MARCAS> MARCAS { get; set; }
        public DbSet<CATEGORIAS> CATEGORIAS { get; set; }
        public DbSet<TIPOSENVIOS> TIPOSENVIOS { get; set; }


        public MyDBContext(DbContextOptions <MyDBContext>options):base(options) { }    
    }
}
