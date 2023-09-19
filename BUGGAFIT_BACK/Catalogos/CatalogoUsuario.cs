using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoUsuario : ICatalogoUsuarios
    {
        List<Usuario> ICatalogoUsuarios.ListarUsuarios()
        {
			try
			{
                using (var db = ConexionBD.ConexionBD.Instance)
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

        void ICatalogoUsuarios.ActualizarUsuario(Usuario employee)
        {
            throw new NotImplementedException();
        }

        Task ICatalogoUsuarios.ActualizarUsuarioAsync(Usuario employee)
        {
            throw new NotImplementedException();
        }

        Usuario ICatalogoUsuarios.AgregarUsuario(Usuario employee)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> ICatalogoUsuarios.AgregarUsuarioAsync(Usuario employee)
        {
            throw new NotImplementedException();
        }

        void ICatalogoUsuarios.BorrarUsuario(int Id)
        {
            throw new NotImplementedException();
        }

        Task ICatalogoUsuarios.BorrarUsuarioAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Usuario ICatalogoUsuarios.ListarUsuario(int Id)
        {
            throw new NotImplementedException();
        }

        Task<Usuario> ICatalogoUsuarios.ListarUsuarioAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<List<Usuario>> ICatalogoUsuarios.ListarUsuariosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
