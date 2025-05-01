using CarritoCompras.Models;

namespace CarritoCompras.Services.ProductoService
{
    public interface IProductoService
    {
        List<Producto> GetAll();
        List<Producto> ObtenerPorFiltro(string name = "");
        Task<Producto?> ObtenerProductoPorId(int id);
    }
}
