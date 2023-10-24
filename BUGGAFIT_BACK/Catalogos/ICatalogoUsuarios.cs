using BUGGAFIT_BACK.Clases;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoUsuarios
    {
        Task<Usuario> AgregarUsuarioAsync(Usuario employee);
        Task<List<Usuario>> ListarUsuariosAsync();
        Task<Usuario> ActualizarUsuarioAsync(Usuario usuario);

        Task BorrarUsuarioAsync(string cedula);
        Task<Usuario> ListarUsuarioAsync(int Id);
    }
}
