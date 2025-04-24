using CarritoCompras.Services.ProductoService;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {

            _productoService = productoService;

        }


        [HttpGet]
        public IActionResult Index()
        {
            var productos = _productoService.ObtenerTodosAsync();
            return View(productos);
        }

    }
}
