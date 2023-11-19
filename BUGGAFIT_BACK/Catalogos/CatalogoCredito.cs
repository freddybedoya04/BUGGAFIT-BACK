using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoCredito:ICatalogoCredito
    {

        private readonly MyDBContext myDbContext;

        public CatalogoCredito(MyDBContext context)
        {
            myDbContext = context;
        }
        public async Task<ResponseObject> ListarCreditosAsync()
        {
            try
            {
                List<Credito> creditos = new List<Credito>();
                List<Cliente> Clientes =  myDbContext.CLIENTES.Where(x => x.CLI_ESCREDITO == true).Select(cliente => new Cliente
                {
                    CLI_NOMBRE=cliente.CLI_NOMBRE,
                    CLI_ID=cliente.CLI_ID,
                    CLI_UBICACION=cliente.CLI_UBICACION
                }).ToList();
                foreach (Cliente cliente in Clientes)
                {
                    // se buscva las ventas por cliente que sean categorizadas como credito y no esten eliminadas
                    List<VENTAS> ventas=myDbContext.VENTAS.Where(x =>x.CLI_ID==cliente.CLI_ID && x.VEN_ESTADOCREDITO==true
                    && x.VEN_ESTADO==true).OrderByDescending(x => x.VEN_CODIGO).ToList();
                    int[] codigosVentas=ventas.Select(x =>x.VEN_CODIGO).ToArray(); 
                    //separamos todas las ventas y buscamos por cartera(abonos)
                    List<Cartera> carteras = await myDbContext.CARTERAS.Where(d => codigosVentas.Contains(d.VEN_CODIGO) && d.CAR_ESTADO == true).Select(x => new Cartera
                    {
                        CAR_CODIGO = x.CAR_CODIGO,
                        VEN_CODIGO = x.VEN_CODIGO,
                        CAR_FECHACREDITO = x.CAR_FECHACREDITO,
                        CAR_VALORABONADO = x.CAR_VALORABONADO,
                        TIC_CODIGO = x.TIC_CODIGO,
                        TIC_NOMBRE = x.TIPOSCUENTAS.TIC_NOMBRE,
                        CAR_ESTADOCREDITO=x.CAR_ESTADOCREDITO,
                        CAR_ESANULADA=x.CAR_ESANULADA
                    }).OrderByDescending(x => x.CAR_CODIGO).ToListAsync();

                    //creamos el creditos 
                    Credito credito = new Credito();
                        credito.CLI_ID = cliente.CLI_ID;
                        credito.CLI_NOMBRE = cliente.CLI_NOMBRE;
                        credito.Ventas = ventas;
                        credito.Carteras = carteras;
                        credito.TotalVendido = ventas.Sum(x => x.VEN_PRECIOTOTAL);
                        credito.TotalAbonado = carteras.Sum(x => x.CAR_VALORABONADO);
                        credito.DiferenciaTotal = credito.TotalVendido - credito.TotalAbonado;
                    //Se agrega el credito a la lista
                    creditos.Add(credito);
                }
                return ResponseClass.Response(statusCode: 200, data: creditos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseObject> ListarCreditosPorClienteAsync( string id)
        {
            try
            {


                    // se buscva las ventas por cliente que sean categorizadas como credito y no esten eliminadas
                    CLIENTES cliente=myDbContext.CLIENTES.Where(x =>x.CLI_ID==id).First();
                    List<VENTAS> ventas = myDbContext.VENTAS.Where(x => x.CLI_ID == cliente.CLI_ID && x.VEN_ESTADOCREDITO == true
                    && x.VEN_ESTADO == true).OrderByDescending(x => x.VEN_CODIGO).ToList();
                    int[] codigosVentas = ventas.Select(x => x.VEN_CODIGO).ToArray();
                    //separamos todas las ventas y buscamos por cartera(abonos)
                    List<Cartera> carteras = await myDbContext.CARTERAS.Where(d => codigosVentas.Contains(d.VEN_CODIGO) && d.CAR_ESTADO == true).Select(x => new Cartera
                    {
                        CAR_CODIGO = x.CAR_CODIGO,
                        VEN_CODIGO = x.VEN_CODIGO,
                        CAR_FECHACREDITO = x.CAR_FECHACREDITO,
                        CAR_VALORABONADO = x.CAR_VALORABONADO,
                        TIC_CODIGO = x.TIC_CODIGO,
                        TIC_NOMBRE = x.TIPOSCUENTAS.TIC_NOMBRE,
                        CAR_ESANULADA=x.CAR_ESANULADA,
                        CAR_ESTADOCREDITO=x.CAR_ESTADOCREDITO,
                    }).OrderByDescending(x => x.CAR_CODIGO).ToListAsync();

                    //creamos el creditos 
                    Credito credito = new Credito();
                    credito.CLI_ID = cliente.CLI_ID;
                    credito.CLI_NOMBRE = cliente.CLI_NOMBRE;
                    credito.Ventas = ventas;
                    credito.Carteras = carteras;
                    credito.TotalVendido = ventas.Sum(x => x.VEN_PRECIOTOTAL);
                    credito.TotalAbonado = carteras.Sum(x => x.CAR_VALORABONADO);
                    credito.DiferenciaTotal = credito.TotalVendido - credito.TotalAbonado;
                    
                
                return ResponseClass.Response(statusCode: 200, data: credito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
