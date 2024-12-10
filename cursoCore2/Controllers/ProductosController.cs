using AutoMapper;
using cursoCore2API.DTOs;
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
    }
}
