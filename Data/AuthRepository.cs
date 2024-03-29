using System.Security.Cryptography;
using CRUD_Basico.Data.Interfaces;
using CRUD_Basico.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Basico.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {   
            _context = context;
        }

        public async Task<bool> ExisteUsuario(string correo)
        {
            try
            {
                bool existe = await _context.Usuarios.AnyAsync(u=> u.Correo == correo);

                return existe;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Usuario> Login(string correo, string password)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x=> x.Correo == correo);

            if(usuario == null) return null;

            if(!VerifyPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt)) return null;

            return usuario;
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt){
            using(var hmac = new HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<Usuario> Registrar(Usuario usuario, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
        
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
            using(var hmac = new HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}