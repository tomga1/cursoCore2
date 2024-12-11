using AutoMapper;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Getproductos()
        {
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


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CrearProducto([FromBody] ProductoInsertDto CrearProductoDto)
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

            if (!_prodRepo.CrearProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {producto.nombre}");
                return StatusCode(404, ModelState);
            }
            return CreatedAtRoute("GetProducto", new { productoId = producto.idProducto }, producto);
        }



        [HttpPatch("{productoId:int}", Name = "ActualizarPatchPelicula")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ActualizarPatchPelicula (int productoId, [FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productoDto == null || productoId != productoDto.idProducto)
            {
                return BadRequest(ModelState);
            }


            var categoriaExistente = _prodRepo.GetProducto(productoId);

            if (categoriaExistente == null)
            {
                return NotFound($"No se encontro la categoria con ID {productoId}");
            }

            var producto = _mapper.Map<Producto>(productoDto);

            if (!_prodRepo.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {producto.nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


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
            var listaProductos = _prodRepo.GetProductosEnCategoria(categoriaId);
            if(listaProductos == null)
            {
                return NotFound();
            }

            var itemProducto = new List<ProductoDto>();
            foreach(var producto in listaProductos)
            {
                itemProducto.Add(_mapper.Map<ProductoDto>(producto));
            }

            return Ok(itemProducto);
        }

        [HttpGet("Buscar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Buscar(string nombre)
        {

            try
            {
                var resultado = _prodRepo.BuscarProducto(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicacion");

            }

        }
    }
}
