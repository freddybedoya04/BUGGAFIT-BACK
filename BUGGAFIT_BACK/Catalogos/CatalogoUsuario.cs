﻿using BUGGAFIT_BACK.Clases;
using BUGGAFIT_BACK.Modelos;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace BUGGAFIT_BACK.Catalogos
{
    public class CatalogoUsuario : ICatalogoUsuarios
    {
        private readonly MyDBContext myDbContext;

        public CatalogoUsuario(MyDBContext context)
        {
            myDbContext = context;
        }
        Task ICatalogoUsuarios.ActualizarUsuarioAsync(Usuario employee)
        {
            throw new NotImplementedException();
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




        Task<Usuario> ICatalogoUsuarios.ListarUsuarioAsync(int Id)
        {
            throw new NotImplementedException();
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

    }
}
