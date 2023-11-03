using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoClientes : ICatalogoClientes
    {
        private readonly MyDBContext myDbContext;

        public CatalogoClientes(MyDBContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<ResponseObject> ActualizarClienteAsync(Cliente cliente)
        {
            try
            {
                #region Chequeo de datos null
                ArgumentNullException.ThrowIfNull(cliente.CLI_ID, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_NOMBRE, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_TIPOCLIENTE, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_UBICACION, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_DIRECCION, nameof(cliente)); 
                #endregion

                CLIENTES _cliente = new()
                {
                    CLI_ID = cliente.CLI_ID,
                    CLI_NOMBRE = cliente.CLI_NOMBRE,
                    CLI_TIPOCLIENTE = cliente.CLI_TIPOCLIENTE,
                    CLI_UBICACION = cliente.CLI_UBICACION,
                    CLI_DIRECCION = cliente.CLI_DIRECCION,
                    CLI_ESTADO = cliente.CLI_ESTADO,
                };
                myDbContext.Entry(_cliente).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.CLI_ID))
                    return ResponseClass.Response(statusCode: 400, message: $"El cliente con el codigo {cliente.CLI_ID} no existe.");
                throw;
            }
            catch (Exception) { throw; }

            return ResponseClass.Response(statusCode: 204, message: $"Cliente Actualizado Exitosamente.");
        }

        public async Task<ResponseObject> BorrarClienteAsync(string Id)
        {
            try
            {
                var cliente = await myDbContext.CLIENTES.FindAsync(Id);
                if (cliente == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El cliente con el codigo {Id} no existe.");

                cliente.CLI_ESTADO = false;
                myDbContext.Entry(cliente).State = EntityState.Modified;
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Cliente Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> RemoverClienteAsync(string Id)
        {
            try
            {
                var cliente = await myDbContext.CLIENTES.FindAsync(Id);
                if (cliente == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El cliente con el codigo {Id} no existe.");

                myDbContext.CLIENTES.Remove(cliente);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 204, message: $"Cliente Eliminado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> CrearClienteAsync(Cliente cliente)
        {
            try
            {
                #region Chequeo de datos null
                ArgumentNullException.ThrowIfNull(cliente.CLI_ID, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_NOMBRE, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_TIPOCLIENTE, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_UBICACION, nameof(cliente));
                ArgumentNullException.ThrowIfNull(cliente.CLI_DIRECCION, nameof(cliente));
                #endregion

                CLIENTES _cliente = new()
                {
                    CLI_ID = cliente.CLI_ID,
                    CLI_NOMBRE = cliente.CLI_NOMBRE,
                    CLI_TIPOCLIENTE = cliente.CLI_TIPOCLIENTE,
                    CLI_UBICACION = cliente.CLI_UBICACION,
                    CLI_DIRECCION = cliente.CLI_DIRECCION,
                    CLI_FECHACREACION = DateTime.Now,
                    CLI_ESTADO = cliente.CLI_ESTADO,
                    CLI_ESCREDITO = cliente.CLI_ESCREDITO,
                };
                myDbContext.CLIENTES.Add(_cliente);
                await myDbContext.SaveChangesAsync();

                return ResponseClass.Response(statusCode: 201, message: $"Cliente Creado Exitosamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarClientePorIDAsync(string Id)
        {
            try
            {
                var cliente = await myDbContext.CLIENTES.FindAsync(Id);
                if (cliente == null)
                    return ResponseClass.Response(statusCode: 400, message: $"El cliente con el codigo {Id} no existe.");
                return ResponseClass.Response(statusCode: 200, data: cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObject> ListarClientesAsync()
        {
            try
            {
                var clientes = await myDbContext.CLIENTES.Where(x => x.CLI_ESTADO == true).ToListAsync();
                if (clientes == null || !clientes.Any())
                    return ResponseClass.Response(statusCode: 204, message: "No hay clientes");

                return ResponseClass.Response(statusCode: 200, data: clientes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ClienteExists(string id)
        {
            return myDbContext.CLIENTES.Any(e => e.CLI_ID == id);
        }
    }
}
