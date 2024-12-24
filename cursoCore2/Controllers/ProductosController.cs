using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cursoCore2API.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IproductoRepository _prodRepo;
        private readonly IMapper _mapper;
        public ProductosController(IproductoRepository prodRepo, IMapper mapper)
        {
            _prodRepo = prodRepo;
            _mapper = mapper;

        }


        //VERSION 1 SIN PAGINADO 

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult Getproductos()
        //{
        //    var listaProductos = _prodRepo.GetProductos();

        //    var listaProductosDto = new List<ProductoDto>();

        //    foreach (var lista in listaProductos)
        //    {
        //        listaProductosDto.Add(_mapper.Map<ProductoDto>(lista));
        //    }
        //    return Ok(listaProductosDto);
        //}


        //VERSION 2 CON PAGINADO
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Getproductos()
        {

            try
            {
                var totalProductos = _prodRepo
            }
            catch (Exception)
            {

                throw;
            }

            var listaProductos = _prodRepo.GetProductos();

            var listaProductosDto = new List<ProductoDto>();

            foreach (var lista in listaProductos)
            {
                listaProductosDto.Add(_mapper.Map<ProductoDto>(lista));
            }
            return Ok(listaProductosDto);
        }


        [HttpGet("{productoId:int}", Name = "GetProducto")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoriaWithId(int productoId)
        {
            var itemCategoria = _prodRepo.GetProducto(productoId);

            if (itemCategoria == null)
            {
                return NotFound();
            }

            var itemProductoDto = _mapper.Map<ProductoDto>(itemCategoria);


            return Ok(itemProductoDto);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearProducto([FromForm] ProductoInsertDto CrearProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            if (CrearProductoDto == null)
            {
                return BadRequest(ModelState); 
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            if (_prodRepo.ExisteProducto(CrearProductoDto.nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe! ");
                return StatusCode(404, ModelState);
            }

            var producto = _mapper.Map<Producto>(CrearProductoDto);

            //if (!_prodRepo.CrearProducto(producto))
            //{
            //    ModelState.AddModelError("", $"Algo salió mal guardando el registro {producto.nombre}");
            //    return StatusCode(404, ModelState);
            //}

            //Subir archivo 

            if (CrearProductoDto.Imagen != null)
            {
                string nombreArchivo = producto.idProducto + System.Guid.NewGuid().ToString() + Path.GetExtension(CrearProductoDto.Imagen.FileName) ;
                string rutaArchivo = @"wwwroot\ImagenesProductos\" + nombreArchivo;

                var ubicacionDirectorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                FileInfo file = new FileInfo(ubicacionDirectorio);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(ubicacionDirectorio, FileMode.Create))
                {
                    CrearProductoDto.Imagen.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                producto.RutaImagen = baseUrl + "/ImagenesProductos/" + nombreArchivo;
                producto.RutaLocalImagen = rutaArchivo; 



            }
            else
            {
                producto.RutaImagen = "https://placehold.co/600x400"; 
            }

            _prodRepo.CrearProducto(producto);  

            
            return CreatedAtRoute("GetProducto", new { productoId = producto.idProducto }, producto);
        }



        [Authorize(Roles = "Admin")]
        [HttpPatch("{productoId:int}", Name = "ActualizarPatchPelicula")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchPelicula (int productoId, [FromForm] ProductoUpdateDto ActualizarProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ActualizarProductoDto == null || productoId != ActualizarProductoDto.idProducto)
            {
                return BadRequest(ModelState);
            }


            var categoriaExistente = _prodRepo.GetProducto(productoId);

            if (categoriaExistente == null)
            {
                return NotFound($"No se encontro la categoria con ID {productoId}");
            }

            var producto = _mapper.Map<Producto>(ActualizarProductoDto);

            //if (!_prodRepo.ActualizarProducto(producto))
            //{
            //    ModelState.AddModelError("", $"Algo salió mal actualizando el registro {producto.nombre}");
            //    return StatusCode(500, ModelState);
            //}

            if (ActualizarProductoDto.Imagen != null)
            {
                string nombreArchivo = producto.idProducto + System.Guid.NewGuid().ToString() + Path.GetExtension(ActualizarProductoDto.Imagen.FileName);
                string rutaArchivo = @"wwwroot\ImagenesProductos\" + nombreArchivo;

                var ubicacionDirectorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                FileInfo file = new FileInfo(ubicacionDirectorio);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(ubicacionDirectorio, FileMode.Create))
                {
                    ActualizarProductoDto.Imagen.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                producto.RutaImagen = baseUrl + "/ImagenesProductos/" + nombreArchivo;
                producto.RutaLocalImagen = rutaArchivo;



            }
            else
            {
                producto.RutaImagen = "https://placehold.co/600x400";
            }

            _prodRepo.ActualizarProducto(producto);
            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{productoId:int}", Name = "BorrarProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarProducto(int productoId)
        {


            if (!_prodRepo.ExisteProducto(productoId))
            {
                return NotFound();

            }


            var producto = _prodRepo.GetProducto(productoId);


            if (!_prodRepo.BorrarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {producto.nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("GetProductosenCategoria/{categoriaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]



        public IActionResult GetProductosenCategoria(int categoriaId)
        {
            try
            {
                var listaProductos = _prodRepo.GetProductosEnCategoria(categoriaId);

                if (listaProductos == null || !listaProductos.Any())
                {
                    return NotFound($"No se encontraron productos en la categoria con ID {categoriaId}.");
                }

                var itemProducto = listaProductos.Select(producto => _mapper.Map<ProductoDto>(producto)).ToList();

                //foreach(var producto in listaProductos)
                //{
                //    itemProducto.Add(_mapper.Map<ProductoDto>(producto));
                //}

                return Ok(itemProducto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuparando datos de la aplicacion");
            }

        }

        [HttpGet("Buscar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Buscar(string nombre)
        {

            try
            {
                var productos = _prodRepo.BuscarProducto(nombre);
                if (!productos.Any())
                {
                    return NotFound($"No se encontraron productos que coincidan con los criterios de busqueda");
                }
                var productosDto = _mapper.Map<IEnumerable<ProductoDto>>(productos);

                return Ok(productosDto);    
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicacion");

            }

        }
    }
}
