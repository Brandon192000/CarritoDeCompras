using CarritoCompras.Models;

namespace CarritoCompras.Services.UsuarioService
{
    public interface IUsuarioService
    {

        Task<Usuario?> ValidarUsuarioAsync(string correo, string contrasenia);

    }
}
