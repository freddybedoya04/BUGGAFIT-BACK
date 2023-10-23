using BUGGAFIT_BACK.Clases;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoUsuarios
    {
        Usuario AgregarUsuario(Usuario employee);
        List<Usuario> ListarUsuarios();
        void ActualizarUsuario(Usuario employee);
        void BorrarUsuario(int Id);
        Usuario ListarUsuario(int Id);

        // Async Methods
        Task<Usuario> AgregarUsuarioAsync(Usuario employee);
        Task<List<Usuario>> ListarUsuariosAsync();
        Task ActualizarUsuarioAsync(Usuario employee);
        Task BorrarUsuarioAsync(int Id);
        Task<Usuario> ListarUsuarioAsync(int Id);
    }
}
