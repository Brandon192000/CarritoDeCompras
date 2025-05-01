using CarritoCompras.Models;
using CarritoCompras.Services.CarritoService;
using CarritoCompras.Services.ProductoService;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    public class CarritoController1 : Controller
    {
        private readonly ICarritoService _carritoService;
        private readonly IProductoService _productoService;

        public CarritoController1(ICarritoService carritoService, IProductoService productoService)
        {
            _carritoService = carritoService;
            _productoService = productoService;
        }

        // ✅ Mostrar el contenido del carrito
        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión para ver su carrito.";
                return RedirectToAction("Index", "Producto");
            }

            var carrito = await _carritoService.ObtenerCarritoPorUsuario(usuarioId.Value);

            if (carrito == null || carrito.DetallesCarrito == null || !carrito.DetallesCarrito.Any())
            {
                TempData["Mensaje"] = "🛒 Su carrito está vacío.";
                return View(new List<CarritoDetalle>());
            }

            foreach (var detalle in carrito.DetallesCarrito)
            {
                detalle.Producto = _productoService.GetAll().FirstOrDefault(p => p.Id == detalle.IdProducto);
            }

            return View(carrito.DetallesCarrito);
        }
    }
}
