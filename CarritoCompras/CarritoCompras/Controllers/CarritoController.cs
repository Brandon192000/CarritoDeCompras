using CarritoCompras.Models;
using CarritoCompras.Services.CarritoService;
using CarritoCompras.Services.ProductoService;
using Microsoft.AspNetCore.Mvc;

namespace CarritoCompras.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoService _carritoService;
        private readonly IProductoService _productoService;

        public CarritoController(ICarritoService carritoService, IProductoService productoService)
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
                return RedirectToAction("Login", "Usuario");
            }

            var carrito = await _carritoService.ObtenerCarritoPorUsuario(usuarioId.Value);

            if (carrito == null || carrito.DetallesCarrito == null || !carrito.DetallesCarrito.Any())
            {
                TempData["Mensaje"] = "🛒 Su carrito está vacío.";
                return View(new List<CarritoDetalle>());
            }

            var productos = _productoService.GetAll().ToDictionary(p => p.Id);

            foreach (var detalle in carrito.DetallesCarrito)
            {
                detalle.Producto = productos.GetValueOrDefault(detalle.IdProducto);
            }

            return View(carrito.DetallesCarrito.ToList());

        }

        [HttpPost]
        public async Task<IActionResult> EliminarDelCarrito(int idProducto)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {


                TempData["Error"] = "Debe iniciar sesión.";
                return RedirectToAction("Login", "Usuario");


            }

            var producto = await _productoService.ObtenerProductoPorId(idProducto);

            if (producto == null)
            {


                TempData["Error"] = "Producto no encontrado.";
                return RedirectToAction("Index");


            }

            await _carritoService.EliminarProductoCarrito(producto, usuarioId.Value);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RestarCantidadProducto(int idProducto)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión.";
                return RedirectToAction("Login", "Usuario");
            }

            var producto = await _productoService.ObtenerProductoPorId(idProducto);

            if (producto == null)
            {
                TempData["Error"] = "Producto no encontrado.";
                return RedirectToAction("Index");
            }

            await _carritoService.RestarCantidadProducto(producto, usuarioId.Value);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int idProducto)
        {

            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {


                TempData["Error"] = "Debe iniciar sesión.";
                return RedirectToAction("Login", "Usuario");


            }
            if (idProducto == 0)
            {
                TempData["Error"] = "Producto no encontrado.";
                return RedirectToAction("Index");
            }

            await _carritoService.AgregarProductoAlCarrito(idProducto, (int)usuarioId);

            return RedirectToAction("Index");
        }
    }
}
