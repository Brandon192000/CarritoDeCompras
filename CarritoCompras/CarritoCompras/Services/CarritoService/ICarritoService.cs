using CarritoCompras.Models;

namespace CarritoCompras.Services.CarritoService
{
    public interface ICarritoService
    {

        Task<Producto> AgregarProductoAlCarrito(int idProducto, int idUsuario);

        Task<Carrito?> ObtenerCarritoPorUsuario(int idUsuario);

        Task<CarritoDetalle> EliminarProductoCarrito(Producto producto, int idUsuario);

        Task<CarritoDetalle> RestarCantidadProducto(Producto producto, int idUsuario);
    }
}
