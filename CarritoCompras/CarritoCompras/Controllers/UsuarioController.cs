using CarritoCompras.Data;
using CarritoCompras.Models;
using CarritoCompras.Services.UsuarioService;
using CarritoCompras.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarritoCompras.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {

            _usuarioService = usuarioService;

        }

        [HttpGet]
        public IActionResult Login()
        {

            return View(new LoginViewModel());

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (string.IsNullOrEmpty(login.Correo) || string.IsNullOrWhiteSpace(login.Contrasenia))
            {
                ModelState.AddModelError("", "El correo electrónico o contraseña no son válidos.");
                return View(login);
            }

            var usuarioRegistrado = await _usuarioService.ValidarUsuarioAsync(login.Correo, login.Contrasenia);

            if (usuarioRegistrado == null)
            {
                ModelState.AddModelError("", "El usuario no se encuentra registrado.");
                return View(login);
            }

            // Guardar ID y Nombre del usuario en sesión
            HttpContext.Session.SetInt32("UsuarioId", usuarioRegistrado.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuarioRegistrado.Nombre);

            // Verificar si los valores se guardaron correctamente
            Console.WriteLine("UsuarioId en sesión: " + HttpContext.Session.GetString("UsuarioId"));
            Console.WriteLine("UsuarioNombre en sesión: " + HttpContext.Session.GetString("UsuarioNombre"));

            // Redirigir al index de productos
            return RedirectToAction("Index", "Producto");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Limpiar toda la sesión
            HttpContext.Session.Clear();
            // Redirigir al login
            return RedirectToAction("Login", "Usuario");
        }
    }

}

