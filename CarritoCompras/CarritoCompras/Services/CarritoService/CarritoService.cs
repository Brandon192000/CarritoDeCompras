using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarritoCompras.Services.CarritoService
{
    public class CarritoService : ICarritoService
    {

        private readonly DBAppContext _context;

        public CarritoService(DBAppContext context)
        {

            _context = context;

        }

        public async Task<Producto> AgregarProductoAlCarrito(int idProducto, int idUsuario)
        {
            try
            {
                // 1. Buscar el producto
                var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == idProducto);

                if (producto == null)
                {
                    return null;
                }
                 
                // 2. Buscar carrito existente del usuario 
                var carrito = await _context.Carritos
                    .Include(c => c.DetallesCarrito)
                    .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario && c.Estado == "Activo");

                // 3. Si no existe, crear nuevo carrito
                if (carrito == null)
                {
                    carrito = new Carrito
                    {
                        IdUsuario = idUsuario,
                        FechaCreacionCarrito = DateTime.Now,
                        Estado = "Activo", // Asegúrate de que el estado sea "Activo"
                    };

                    _context.Carritos.Add(carrito);
                    await _context.SaveChangesAsync(); // Necesario para obtener el Id del carrito
                }

                // 4. Buscar si el producto ya está en el carrito
                var detalleExistente = await _context.CarritoDetalles
                    .FirstOrDefaultAsync(cd => cd.IdCarrito == carrito.Id && cd.IdProducto == idProducto);

                if (detalleExistente != null)
                {

                    // Si ya existe, aumentar la cantidad
                    detalleExistente.Cantidad += 1;

                }
                else
                {
                    // Si no existe, agregar nuevo detalle
                    var nuevoDetalle = new CarritoDetalle
                    {
                        IdCarrito = carrito.Id,
                        IdProducto = idProducto,
                        Cantidad = 1,
                        PrecioProducto = producto.Precio ?? 0
                    };

                    _context.CarritoDetalles.Add(nuevoDetalle);
                }

                await _context.SaveChangesAsync();

                return producto;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Carrito?> ObtenerCarritoPorUsuario(int idUsuario)
        {
            return await _context.Carritos
                .Include(c => c.DetallesCarrito)
                .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario && c.Estado == "Activo");
        }


    }
}
