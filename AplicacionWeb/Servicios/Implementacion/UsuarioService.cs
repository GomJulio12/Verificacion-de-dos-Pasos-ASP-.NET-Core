using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Servicios.Contrato;
using AplicacionWeb.Models;

namespace AplicacionWeb.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly EncriptacionContext _dbContext;
        public UsuarioService(EncriptacionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string password)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(u => u.Correo == correo && u.Password == password)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
