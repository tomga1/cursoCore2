using cursoCoreMVC.Models;
using cursoCoreMVC.Repository.IRepository;
using cursoCoreMVC.Services;
using cursoCoreMVC.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;

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

        [HttpGet]
        public async Task<IActionResult> Getproductos()
        {

            return Json(new { data = await _repoProducto.GetProductosAsync(Constante.RutaProductosApi) });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var marcas = new List<Marcas>
    {
        new Marcas { idMarca = 1, nombre = "Marca A" },
        new Marcas { idMarca = 2, nombre = "Marca B" },
        new Marcas { idMarca = 3, nombre = "Marca C" }
    };

            // Datos ficticios para las categorías
            var categorias = new List<Categoria>
    {
        new Categoria { categoriaId = 1, categoria_nombre = "Categoría 1" },
        new Categoria { categoriaId = 2, categoria_nombre = "Categoría 2" },
        new Categoria { categoriaId = 3, categoria_nombre = "Categoría 3" }
    };

            // Llenar los ViewBag con los datos ficticios
            ViewBag.Marcas = new SelectList(marcas, "idMarca", "nombre");
            ViewBag.Categorias = new SelectList(categorias, "categoriaId", "categoria_nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Productos productos)
        {
            if (ModelState.IsValid)
            {
                _repoProducto.CrearAsync(Constante.RutaProductosApi, productos);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
