using CarritoCompras.Data;
using CarritoCompras.Models;
using CarritoCompras.Services.UsuarioService;
using CarritoCompras.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

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

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login) 
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (string.IsNullOrEmpty(login.Correo) && string.IsNullOrWhiteSpace(login.Contrasenia))
            {
                ModelState.AddModelError("", "El correo electronico o contraseña no es valido. Debe verificar que no haya espacios ni este vacio");
                return View(login);
            }

            var usuarioRegistrado = await _usuarioService.
                                          ValidarUsuarioAsync(login.Correo, login.Contrasenia);

            if(usuarioRegistrado == null)
            {
                ModelState.AddModelError("", "El usuario no se encuentra registradoo");
                return View(login);
            }


            HttpContext.Session.SetString("UsuarioId", usuarioRegistrado.Id.ToString()); // Guardar usuario en sesión, cookie, claims, etc.


            return RedirectToAction("Index", "Home");

        }

    }
}
