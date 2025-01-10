using cursoCore2API.DTOs;
using cursoCore2API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AutoMapper;
using cursoCore2API.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using XAct;
using cursoCore2API.Services.IServices;
using XAct.Messages;
using XAct.Services;



namespace cursoCore2API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/categorias")]
    [ApiController]
    [AllowAnonymous]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoriaService _service;
        private readonly IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto> _serviceBase;
        private readonly IMessageService _messageService;

        public CategoriasController(ICategoriaRepository repository, IMapper mapper, ICategoriaService service, IMessageService messageService, IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto> serviceBase)
        {
            _repository = repository;  
            _mapper = mapper; 
            _service = service; 
            _serviceBase = serviceBase;
            _messageService = messageService;   
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategorias()
        {
            var ListCategorias = await _service.GetCategoriasAsync();

            return Ok(ListCategorias);  
        }


        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoriaById(int categoriaId)
        {
           
                var itemCategoria = await _service.GetCategoriaByIdAsync(categoriaId);

                if (itemCategoria == null)
                {
                var message = _messageService.GetMessage("CategoriaNoEncontrada");
                    return NotFound(new { message});
                }

                var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria); 

                return Ok(itemCategoriaDto);
            
        }




        //[Authorize(Roles = "admin")]
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
            

            if (_repository.ExisteCategoria(crearCategoriaDto.categoria_nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe! ");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(crearCategoriaDto);

            if (!_repository.Add(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {categoria.categoria_nombre}");
                return StatusCode(404, ModelState);
            }
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.categoriaId }, categoria);
        }














        //[Authorize(Roles = "admin")]
        //[HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public IActionResult ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaDto CategoriaDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (CategoriaDto == null || categoriaId != CategoriaDto.categoriaId)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var categoriaExistente = _repository.GetCategoria(categoriaId);

        //    if (categoriaExistente == null)
        //    {
        //        return NotFound($"No se encontro la categoria con ID {categoriaId}");
        //    }

        //    var categoria = _mapper.Map<Categoria>(CategoriaDto);

        //    if (!_repository.Update(categoria))
        //    {
        //        ModelState.AddModelError("", $"Algo salió mal actualizando el registro {categoria.categoria_nombre}");
        //        return StatusCode(500, ModelState);
        //    }
        //    return NoContent();
        



    //[Authorize(Roles = "admin")]
    //[HttpPut("{categoriaId:int}", Name = "ActualizarPutCategoria")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public IActionResult ActualizarPutCategoria(int categoriaId, [FromBody] CategoriaDto CategoriaDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    if (CategoriaDto == null || categoriaId != CategoriaDto.categoriaId)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    var categoriaExistente = _repository.GetCategoria(categoriaId);

    //    if (categoriaExistente == null)
    //    {
    //        return NotFound($"No se encontro la categoria con ID {categoriaId}");
    //    }


    //    var categoria = _mapper.Map<Categoria>(CategoriaDto);


    //    if (!_repository.Update(categoria))
    //    {
    //        ModelState.AddModelError("", $"Algo salió mal actualizando el registro {categoria.categoria_nombre}");
    //        return StatusCode(500, ModelState);
    //    }
    //    return NoContent();
    //}


        [Authorize(Roles = "admin")]
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrarCategoria(int categoriaId)
        {
            var categoriaExiste = await _service.ExisteCategoriaAsync(categoriaId);

            if(!categoriaExiste)
            {
                return NotFound();  
            }
            var resultado = await _service.DeleteAsync(categoriaId);
            if (!resultado)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la categoría.");
            }

            return NoContent();

        }
    }
}




