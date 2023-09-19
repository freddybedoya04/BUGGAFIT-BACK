using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoVentas : ICatalogoVentas
    {
        private readonly MyDBContext myDbContext;

        public CatalogoVentas(MyDBContext context)
        {
            myDbContext = context;
        }

        public void ActualizarVenta(Ventas venta)
        {
            throw new NotImplementedException();
        }

        public Task ActualizarVentaAsync(Ventas venta)
        {
            throw new NotImplementedException();
        }

        public void BorrarVenta(int Id)
        {
            throw new NotImplementedException();
        }

        public Task BorrarVentaAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Ventas CrearVenta(Ventas venta)
        {
            throw new NotImplementedException();
        }

        public Task<Ventas> CrearVentaAsync(Ventas venta)
        {
            throw new NotImplementedException();
        }

        public Ventas ListarVentaPorID(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> ListarVentaPorIDAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Ventas> ListarVentas()
        {
            throw new NotImplementedException();
        }

        public Task<List<Ventas>> ListarVentasAsync()
        {
            throw new NotImplementedException();
        }
        private bool VentasExists(int codigo)
        {
            return myDbContext.VENTAS.Any(e => e.VEN_CODIGO == codigo);
        }
    }
}
