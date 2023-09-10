using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoUsuario
    {

        private readonly MyDBContext myDbContext;

        public CatalogoUsuario(MyDBContext context)
        {
            myDbContext = context;
        }
        public List<Usuario> ListarUsuarios()
        {
			try
			{
                using (var db=myDbContext)
                {
                    List<Usuario> usuarios = db.USUARIOS.Select(x => new Usuario
                    {
                        USU_CEDULA = x.USU_CEDULA,
                        USU_FECHAACTUALIZACION = x.USU_FECHAACTUALIZACION,
                        USU_NOMBRE = x.USU_NOMBRE,
                        USU_CONTRASEÑA = x.USU_CONTRASEÑA,
                        USU_ROL = x.USU_ROL,
                    }).ToList();
                    return usuarios;
                }


			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
