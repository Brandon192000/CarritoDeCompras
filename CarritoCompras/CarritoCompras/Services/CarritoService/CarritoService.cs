using CarritoCompras.Data;
using CarritoCompras.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

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


        public async Task<Carrito> ObtenerCarritoPorUsuario(int usuarioId)
        {
            try
            {

                var carrito = await _context!.Carritos!
                              .Include(c => c.DetallesCarrito!)
                              .ThenInclude(d => d.Producto!)
                              .FirstOrDefaultAsync(c => c.IdUsuario == usuarioId)
                               ?? new Carrito();

                return carrito;

            }
            catch (Exception)
            {

                throw;
            }
        }
            

        public async Task<CarritoDetalle> EliminarProductoCarrito(Producto producto, int idUsuario)
        {

            try
            {

                var carrito = await _context.Carritos.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

                if (carrito == null)
                {

                    return null;

                }

                var productoEliminar = await _context.CarritoDetalles
                                      .FirstOrDefaultAsync(p => p.IdProducto == producto.Id);

                if (productoEliminar == null)
                {
                    return null;
                }

                _context.CarritoDetalles.Remove(productoEliminar);
                await _context.SaveChangesAsync();

                return productoEliminar;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<CarritoDetalle> RestarCantidadProducto(Producto producto, int idUsuario)
        {

            try
            {

                if (producto == null)
                {

                    return null;

                }

                var carrito = await _context.Carritos.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);

                if (carrito == null)
                {

                    return null;

                }

                var productoRestar = await _context.CarritoDetalles
                                      .FirstOrDefaultAsync(p => p.IdProducto == producto.Id);

                if (productoRestar == null)
                {

                    return null;

                }

                if (productoRestar.Cantidad >= 1)
                {

                    productoRestar.Cantidad -= 1;
                    _context.CarritoDetalles.Update(productoRestar);

                }
                else
                {

                    _context.Remove(productoRestar);

                }

                await _context.SaveChangesAsync();
                return productoRestar;

            }
            catch (Exception)
            {

                throw;

            }
            
        }
    }
}
