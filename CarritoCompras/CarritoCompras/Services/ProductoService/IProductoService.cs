using CarritoCompras.Models;

namespace CarritoCompras.Services.ProductoService
{
    public interface IProductoService
    {

        List<Producto> ObtenerTodosAsync();

    }
}
