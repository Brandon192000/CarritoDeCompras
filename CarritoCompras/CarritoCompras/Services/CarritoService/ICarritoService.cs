using CarritoCompras.Models;

namespace CarritoCompras.Services.CarritoService
{
    public interface ICarritoService
    {

        Task<Producto> AgregarProductoAlCarrito(int idProducto, int idUsuario);
        Task<Carrito?> ObtenerCarritoPorUsuario(int idUsuario);

    }
}
