using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
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

        public List<Producto> ObtenerTodosAsync()
        {

            return _context.Productos.ToList();

        }

    }
}
