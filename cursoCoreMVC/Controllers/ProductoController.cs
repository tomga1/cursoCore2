using cursoCoreMVC.Models;
using cursoCoreMVC.Repository.IRepository;
using cursoCoreMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace cursoCoreMVC.Controllers
{
    public class ProductoController : Controller
    {
        //private readonly IproductoService _serviceAPI;


        //public ProductoController(IproductoService serviceAPI)
        //{
        //    _serviceAPI = serviceAPI;   
        //}

        //public async Task<IActionResult> Lista()
        //{
        //    var productos = await _serviceAPI.Lista();
        //    return View(productos); 
        //}


        private readonly IProductoRepository _repoProducto;

        public ProductoController(IProductoRepository repoProducto)
        {
            _repoProducto = repoProducto;   
        }


        public IActionResult Index()
        {
            return View(new Productos() { });
        }
    }
}
