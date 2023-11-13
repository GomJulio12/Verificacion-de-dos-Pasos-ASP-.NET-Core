using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Models;

namespace AplicacionWeb.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string password);
        Task<Usuario> SaveUsuario(Usuario modelo);

    }
}
