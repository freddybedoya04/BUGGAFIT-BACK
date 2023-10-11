using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos.Entidad;
using BUGGAFIT_BACK.Modelos;
using Microsoft.EntityFrameworkCore;
using BUGGAFIT_BACK.Migrations;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoInventario : ICatalogoInventario
    {

        private readonly MyDBContext myDbContext;

        public CatalogoInventario(MyDBContext context)
        {
            myDbContext = context;
        }

        public async Task<ResponseObject> ActualizarProductoAsync(Producto producto)
        {
            ArgumentNullException.ThrowIfNull(producto.PRO_CODIGO, nameof(producto));
            try
            {
                PRODUCTOS _producto = new()
                {
                    PRO_CODIGO = producto.PRO_CODIGO ?? throw new ArgumentNullException(nameof(producto), "El codigo del producto no puede ser nulo."),
                    PRO_NOMBRE = producto.PRO_NOMBRE ?? throw new ArgumentNullException(nameof(producto), "El nombre del producto no puede ser nulo."),
                    MAR_CODIGO = Convert.ToInt32(producto.PRO_MARCA),
                    CAT_CODIGO = Convert.ToInt32(producto.PRO_CATEGORIA),
                    PRO_PRECIO_COMPRA = producto.PRO_PRECIO_COMPRA,
                    PRO_PRECIOVENTA_DETAL = producto.PRO_PRECIOVENTA_DETAL,
                    PRO_PRECIOVENTA_MAYORISTA = producto.PRO_PRECIOVENTA_MAYORISTA,
                    PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES,
                    PRO_UNIDADES_MINIMAS_ALERTA = producto.PRO_UNIDADES_MINIMAS_ALERTA,
                    PRO_ACTUALIZACION = DateTime.Now,
                    PRO_ESTADO = producto.PRO_ESTADO,
                };
                myDbContext.Entry(_producto).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteProducto(producto.PRO_CODIGO))
                    return ResponseClass.Response(statusCode: 400, message: $"El producto con el codigo {producto.PRO_CODIGO} no existe.");

                throw;
            }
            catch (Exception) { throw; }
            return ResponseClass.Response(statusCode: 204, message: $"Produto Actualizado Exitosamente.");
        }

        public async Task<ResponseObject> BorrarProductoAsync(int Id)
        {
            try
            {
                var _producto = await myDbContext.PRODUCTOS.FindAsync(Id);
                if (_producto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El producto con el codigo {Id} no existe.");

                _producto.PRO_ESTADO = false;
                myDbContext.Entry(_producto).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Producto Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> RemoverProductoAsync(int Id)
        {
            try
            {
                var _producto = await myDbContext.PRODUCTOS.FindAsync(Id);
                if (_producto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El producto con el codigo {Id} no existe.");

                myDbContext.PRODUCTOS.Remove(_producto);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Producto Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearProductoAsync(Producto producto)
        {
            try
            {
                PRODUCTOS _producto = new()
                {
                    PRO_CODIGO = producto.PRO_CODIGO ?? throw new ArgumentNullException(nameof(producto), "El codigo del producto no puede ser nulo."),
                    PRO_NOMBRE = producto.PRO_NOMBRE ?? throw new ArgumentNullException(nameof(producto), "El nombre del producto no puede ser nulo."),
                    MAR_CODIGO = Convert.ToInt32(producto.PRO_MARCA),
                    CAT_CODIGO = Convert.ToInt32(producto.PRO_CATEGORIA),
                    PRO_PRECIO_COMPRA = producto.PRO_PRECIO_COMPRA,
                    PRO_PRECIOVENTA_DETAL = producto.PRO_PRECIOVENTA_DETAL,
                    PRO_PRECIOVENTA_MAYORISTA = producto.PRO_PRECIOVENTA_MAYORISTA,
                    PRO_UNIDADES_DISPONIBLES = producto.PRO_UNIDADES_DISPONIBLES,
                    PRO_UNIDADES_MINIMAS_ALERTA = producto.PRO_UNIDADES_MINIMAS_ALERTA,
                    PRO_ACTUALIZACION = DateTime.Now,
                    PRO_FECHACREACION = DateTime.Now,
                    PRO_ESTADO = producto.PRO_ESTADO,
                };
                myDbContext.PRODUCTOS.Add(_producto);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Producto Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarMarcasAsync()
        {
            try
            {
                var _marcas = await myDbContext.MARCAS.Where(x => x.MAR_ESTADO == true).ToListAsync();
                if (_marcas == null || !_marcas.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay categorias.");

                return ResponseClass.Response(statusCode: 200, data: _marcas);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarCategoriasAsync()
        {
            try
            {
                var _categorias = await myDbContext.CATEGORIAS.Where(x => x.CAT_ESTADO == true).ToListAsync();
                if (_categorias == null || !_categorias.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay Marcas.");

                return ResponseClass.Response(statusCode: 200, data: _categorias);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarProductoPorIDAsync(string Id)
        {
            try
            {
                var _producto = await myDbContext.PRODUCTOS.FindAsync(Id);
                if (_producto == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El producto con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: _producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarProductosAsync()
        {
            try
            {
                var _productos = await (from p in myDbContext.PRODUCTOS
                                        select new Producto
                                        {
                                            PRO_CODIGO = p.PRO_CODIGO,
                                            PRO_NOMBRE = p.PRO_NOMBRE,
                                            PRO_MARCA = p.MARCA.MAR_NOMBRE,
                                            PRO_CATEGORIA = p.CATEGORIA.CAT_NOMBRE,
                                            PRO_PRECIO_COMPRA = p.PRO_PRECIO_COMPRA,
                                            PRO_PRECIOVENTA_DETAL = p.PRO_PRECIOVENTA_DETAL,
                                            PRO_PRECIOVENTA_MAYORISTA = p.PRO_PRECIOVENTA_MAYORISTA,
                                            PRO_UNIDADES_DISPONIBLES = p.PRO_UNIDADES_DISPONIBLES,
                                            PRO_UNIDADES_MINIMAS_ALERTA = p.PRO_UNIDADES_MINIMAS_ALERTA,
                                            PRO_ACTUALIZACION = p.PRO_ACTUALIZACION,
                                            PRO_FECHACREACION = p.PRO_FECHACREACION,
                                            PRO_ESTADO = p.PRO_ESTADO,
                                        }).ToListAsync();
                if (_productos == null || !_productos.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay Productos.");

                return ResponseClass.Response(statusCode: 200, data: _productos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ExisteProducto(string id)
        {
            return myDbContext.PRODUCTOS.Any(e => e.PRO_CODIGO == id);
        }
    }
}
