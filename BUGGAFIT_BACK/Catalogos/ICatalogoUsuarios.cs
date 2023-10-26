﻿using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BUGGAFIT_BACK.Catalogos
{
    public interface ICatalogoUsuarios
    {
        Task<Usuario> AgregarUsuarioAsync(Usuario employee);
        Task<Usuario> ActualizarUsuarioAsync(Usuario usuario );
        Task<Usuario> BuscarUsuarioPorCedulaAsync(string cedula);
        Task BorrarUsuarioAsync(string cedula);
        Task<List<Usuario>> ListarUsuariosAsync();
        Task<bool> ValidadUsuarioConPermisosAdmin(LoginDTO loginDTO);
    }
}
