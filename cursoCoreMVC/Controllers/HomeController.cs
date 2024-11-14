using cursoCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using cursoCoreMVC.Services;
using System.Security.Cryptography.Xml;
using Microsoft.IdentityModel.Tokens;

namespace cursoCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService_API _serviceAPI;

        public HomeController(IService_API servicioAPI)
        {
            _serviceAPI = servicioAPI;
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                
                await _serviceAPI.Authenticate(username, password);

                if (string.IsNullOrEmpty(Service_API.Token))
                {
                    // Si el token no es válido, mostramos un mensaje de error
                    //ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                    return View();
                }

                // Si la autenticación es exitosa, redirigimos al Home
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Si ocurre algún error en el proceso de autenticación
                ViewBag.ErrorMessage = $"Ocurrió un error: {ex.Message}";
                return View();
            }
        }



        public async Task<IActionResult> Index()
        {

            List<Productos> Lista = await _serviceAPI.Lista();
            return View(Lista);
        }


        [HttpGet]
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            var respuesta = await _serviceAPI.Eliminar(idProducto);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NoContent();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
