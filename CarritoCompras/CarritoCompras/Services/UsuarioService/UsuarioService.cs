using CarritoCompras.Data;
using CarritoCompras.Models;
using CarritoCompras.Services.UsuarioService;
using Microsoft.EntityFrameworkCore;

public class UsuarioService : IUsuarioService
{
    private readonly DBAppContext _context;

    public UsuarioService(DBAppContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ValidarUsuarioAsync(string correo, string contrasenia)
    {

        if (string.IsNullOrEmpty(correo) && string.IsNullOrEmpty(contrasenia)) 
        {
        
            return null;

        }

        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == correo && u.Contrasenia == contrasenia);
    }
}