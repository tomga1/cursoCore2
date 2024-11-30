using cursoCoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace cursoCoreMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IproductoService _serviceAPI;

        public ProductoController(IproductoService serviceAPI)
        {
            _serviceAPI = serviceAPI;   
        }

        public async Task<IActionResult> Lista()
        {
            var productos = await _serviceAPI.Lista();
            return View(productos); 
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
