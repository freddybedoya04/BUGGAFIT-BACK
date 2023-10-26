using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.DTOs;
using BUGGAFIT_BACK.DTOs.Response;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.ModelBinding;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoUsuario : ICatalogoUsuarios
    {
        private readonly MyDBContext myDbContext;

        public CatalogoUsuario(MyDBContext context)
        {
            myDbContext = context;
        }
        public async Task<Usuario> ActualizarUsuarioAsync(Usuario usuario)
        {
            try
            {
                using (var db = myDbContext)
                {
                    var usuarioAActualizar = await db.USUARIOS.FirstOrDefaultAsync(x => x.USU_CEDULA == usuario.USU_CEDULA);
                    if (usuarioAActualizar != null)
                    {
                        usuarioAActualizar.USU_NOMBRE = usuario.USU_NOMBRE;
                        usuarioAActualizar.USU_ROL = usuario.USU_ROL;
                        usuarioAActualizar.USU_FECHAACTUALIZACION = DateTime.Now;

                        if (usuario.USU_CONTRASEÑA != null)  // Comprueba si la contraseña se proporcionó
                        {
                            // Encripta la nueva contraseña solo si se proporciona
                            usuarioAActualizar.USU_CONTRASEÑA = EncriptarContraseña(usuario.USU_CONTRASEÑA);
                        }

                        await db.SaveChangesAsync();
                        return usuario;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<Usuario> AgregarUsuarioAsync(Usuario usuario)
        {
            try
            {
                using (var db = myDbContext)
                {
                    var nuevoUsuario = new USUARIOS
                    {
                        USU_CEDULA = usuario.USU_CEDULA,
                        USU_NOMBRE = usuario.USU_NOMBRE,
                        USU_CONTRASEÑA = EncriptarContraseña(usuario.USU_CONTRASEÑA), // Encriptar la contraseña
                        USU_ROL = usuario.USU_ROL,
                        USU_FECHACREACION = DateTime.Now,
                        USU_FECHAACTUALIZACION = DateTime.Now,
                        USU_ESTADO = true
                    };

                    db.USUARIOS.Add(nuevoUsuario);
                    await db.SaveChangesAsync();
                    return usuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string EncriptarContraseña(string contraseña)
        {
            byte[] salt = Convert.FromBase64String("CGYzqeN4plZekNC88Umm1Q=="); // divide by 8 to convert bits to bytes

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: contraseña!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }




        public async Task BorrarUsuarioAsync(string cedula)
        {
            try
            {
                using (var db = myDbContext)
                {
                    var usuarioAEliminar = db.USUARIOS.FirstOrDefault(x => x.USU_CEDULA == cedula);
                    if (usuarioAEliminar != null)
                    {
                        db.USUARIOS.Remove(usuarioAEliminar);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Usuario> BuscarUsuarioPorCedulaAsync(string cedula)
        {
            try
            {
                using (var db = myDbContext)
                {
                    var usuario = await db.USUARIOS.FirstOrDefaultAsync(x => x.USU_CEDULA == cedula);

                    if (usuario != null)
                    {

                        return new Usuario
                        {
                            USU_CEDULA = usuario.USU_CEDULA,
                            USU_NOMBRE = usuario.USU_NOMBRE,
                            USU_CONTRASEÑA = usuario.USU_CONTRASEÑA,
                            USU_ROL = usuario.USU_ROL,
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            try
            {
                using (var db = myDbContext)
                {
                    List<Usuario> usuarios = await db.USUARIOS.Select(x => new Usuario
                    {
                        USU_CEDULA = x.USU_CEDULA,
                        USU_FECHAACTUALIZACION = x.USU_FECHAACTUALIZACION,
                        USU_NOMBRE = x.USU_NOMBRE,
                        USU_CONTRASEÑA = x.USU_CONTRASEÑA,
                        USU_ROL = x.USU_ROL,
                    }).ToListAsync();
                    return usuarios;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidadUsuarioConPermisosAdmin(LoginDTO loginDTO)
        {
            try
            {

                // search the user in db.
                //var query = await _postgresContext.Usercompanies.Where(x => x.Login == loginDTO.Username && x.State == true).ToListAsync();
                var query = await (from u in myDbContext.USUARIOS
                                   where u.USU_CEDULA == loginDTO.Cedula && u.USU_ESTADO == true
                                   select new Usuario
                                   {
                                       USU_CEDULA = u.USU_CEDULA,
                                       USU_NOMBRE = u.USU_NOMBRE,
                                       USU_CONTRASEÑA = u.USU_CONTRASEÑA,
                                       USU_ROL = u.USU_ROL,
                                       USU_ESTADO = u.USU_ESTADO,
                                   }).ToListAsync();

                if (query.Count <= 0)
                    return false;

                var user = query.FirstOrDefault();
                if (user == null || user.USU_CONTRASEÑA == null)
                    return false;
                if (EncriptarContraseña(loginDTO.Password) != user.USU_CONTRASEÑA)
                    return false;
                if (user.USU_ROL.ToLower() == "admin" || user.USU_ROL.ToLower() == "administrador" || user.USU_ROL.ToLower() == "administrator")
                    return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
