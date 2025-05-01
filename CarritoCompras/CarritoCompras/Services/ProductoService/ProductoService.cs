using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarritoCompras.Services.ProductoService
{
    public class ProductoService : IProductoService
    {

        private readonly DBAppContext _context;

        public ProductoService(DBAppContext context)
        {
            _context = context;
        }

        public List<Producto> GetAll()
        {
            return _context.Productos.Where(p=> !p.Borrado).ToList();
        }

        public List<Producto> ObtenerPorFiltro(string name = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return _context.Productos
                                   .Where(p => !p.Borrado)
                                   .ToList();
                }

                return _context.Productos
                               .Where(p => !p.Borrado && p.Nombre.Contains(name.Trim()))
                               .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Producto?> ObtenerProductoPorId(int id)
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }



    }
}
