using CarritoCompras.Models;
using CarritoCompras.Services.CarritoService;
using CarritoCompras.Services.ProductoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICarritoService _carritoService;

        public ProductoController(IProductoService productoService, ICarritoService carritoService)
        {
            _productoService = productoService;
            _carritoService = carritoService;
        }

        [HttpGet]
        public IActionResult Index(string filtro)
        {
            var productos = string.IsNullOrWhiteSpace(filtro)
                ? _productoService.GetAll()
                : _productoService.ObtenerPorFiltro(filtro.Trim());

            return View(productos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int idProducto)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión para agregar productos al carrito.";
                return RedirectToAction("Login", "Usuario");
            }

            var producto = await _carritoService.AgregarProductoAlCarrito(idProducto, usuarioId.Value);

            TempData["Mensaje"] = producto != null
                ? "Producto agregado al carrito correctamente."
                : "Producto no encontrado o no disponible.";

            return RedirectToAction("Index");
        }
    }
}
