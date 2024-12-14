using cursoCore2API.DTOs;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AutoMapper;
using cursoCore2API.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace cursoCore2API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper; 
        public CategoriasController(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper; 

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _repository.GetCategorias();

            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista));
            }
            return Ok(listaCategoriasDto);  
        }


        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoriaWithId(int categoriaId)
        {
            var itemCategoria = _repository.GetCategoria(categoriaId);

            if (itemCategoria == null)
            {
                return NotFound();
            }

            var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria); 


            return Ok(itemCategoriaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearCategoria([FromBody] CategoriaInsertDto crearCategoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            if (crearCategoriaDto == null)
            {
                return BadRequest(ModelState); 
            }
            

            if (_repository.ExisteCategoria(crearCategoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe! ");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(crearCategoriaDto);

            if (!_repository.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {categoria.Nombre}");
                return StatusCode(404, ModelState);
            }
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.categoriaId }, categoria);
        }

        [HttpPatch("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaDto CategoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CategoriaDto == null || categoriaId != CategoriaDto.categoriaId)
            {
                return BadRequest(ModelState);
            }


            var categoriaExistente = _repository.GetCategoria(categoriaId);

            if (categoriaExistente == null)
            {
                return NotFound($"No se encontro la categoria con ID {categoriaId}");
            }

            var categoria = _mapper.Map<Categoria>(CategoriaDto);

            if (!_repository.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }



        [HttpPut("{categoriaId:int}", Name = "ActualizarPutCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPutCategoria(int categoriaId, [FromBody] CategoriaDto CategoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CategoriaDto == null || categoriaId != CategoriaDto.categoriaId)
            {
                return BadRequest(ModelState);
            }

            var categoriaExistente = _repository.GetCategoria(categoriaId);

            if (categoriaExistente == null)
            {
                return NotFound($"No se encontro la categoria con ID {categoriaId}");
            }


            var categoria = _mapper.Map<Categoria>(CategoriaDto);


            if (!_repository.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarCategoria(int categoriaId)
        {


            if (!_repository.ExisteCategoria(categoriaId))
            {
                return NotFound();

            }


            var categoria = _repository.GetCategoria(categoriaId); 



            if (!_repository.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
