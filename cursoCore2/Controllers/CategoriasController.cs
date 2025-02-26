using cursoCore2API.DTOs;
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
using System.Runtime.CompilerServices;
using Serilog;  


namespace cursoCore2API.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    [AllowAnonymous]
    public class CategoriasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _service;
        private readonly IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto> _serviceBase;
        private readonly IMessageService _messageService;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(IMapper mapper, ICategoriaService service, IMessageService messageService, IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto> serviceBase, ILogger<CategoriasController> logger)
        {
            _mapper = mapper; 
            _service = service; 
            _serviceBase = serviceBase;
            _messageService = messageService;  
            _logger = logger;
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
                _logger.LogInformation("Inicio del metodo GET Categorias"); 
                var ListCategorias = await _service.GetCategoriasAsync();
                return Ok(ListCategorias);  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categorias con el metodo GET");
                return StatusCode(500, "Error interno del servidor"); 
            }
        }


        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoriaById(int categoriaId)
        {

            try
            {
                _logger.LogInformation("Inicio del método GetCategoriaById. ID: {CategoriaId}", categoriaId);

                if (categoriaId <= 0)
                {
                    _logger.LogWarning("ID inválido: {CategoriaId}", categoriaId);
                    return BadRequest(new { Message = "El ID debe ser un número positivo" });
                }

                var itemCategoria = await _service.GetByIdAsync(categoriaId);

                if (itemCategoria == null)
                {
                var message = _messageService.GetMessage("CategoriaNoEncontrada");
                    return NotFound(new { message});
                }

                var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria); 

                return Ok(itemCategoriaDto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error en GetCategoriaById. ID: {CategoriaId}", categoriaId);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
            
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaInsertDto categoriaInsertDto)
        {
            try
            {
                _logger.LogInformation("Inicio de CrearCategoria - Nombre: {Nombre}", categoriaInsertDto.nombre);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultado = await _serviceBase.AddAsync(categoriaInsertDto); // Usa Dto para crear

                if (resultado == null)
                {
                    return StatusCode(500, new { message = $"Algo salió mal guardando el registro {categoriaInsertDto.nombre}" });
                }

                var categoria = _mapper.Map<Categoria>(resultado); // Mapear a Categoria si es necesario
                return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoriaInsertDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CrearCategoria - Nombre: {Nombre}", categoriaInsertDto?.nombre);
                return StatusCode(500, new { Message = "Error interno al procesar la solicitud" });
            }
        }


        [Authorize(Roles = "admin")]
        [HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaUpdateDto categoriaUpdateDto)
        {

            try
            {
                _logger.LogInformation("Inicio ActualizarPatchCategoria. ID: {CategoriaId}", categoriaId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Datos de entrada inválidos", Errors = ModelState });
                }

                var categoriaExistente = await _service.GetByIdAsync(categoriaId);

                if (categoriaExistente == null)  
                {
                    return NotFound($"No se encontró la categoría con ID {categoriaId}");
                }

                var actualizado = await _service.UpdateAsync(categoriaId, categoriaUpdateDto);

                if (actualizado == null)
                {
                    ModelState.AddModelError("", "Algo salió mal actualizando el registro");
                    return StatusCode(500, ModelState);
                }

                return NoContent(); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ActualizarPatchCategoria. ID: {CategoriaId}", categoriaId);
                return StatusCode(500, new { Message = "Error interno del servidor" });
            }
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrarCategoria(int categoriaId)
        {
            try
            {
                _logger.LogInformation("Iniciando borrado de categoría. ID: {CategoriaId}", categoriaId);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar categoría. ID: {CategoriaId}", categoriaId);
                return StatusCode(500, new
                {
                    Message = "Error interno durante el borrado",
                });
            }
        }


        //[Authorize(Roles = "admin")]
        [HttpPut("{categoriaId:int}", Name = "ActualizarPutCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarPutCategoria(int categoriaId, [FromBody] CategoriaUpdateDto categoriaUpdateDto)
        {

            try
            {
                _logger.LogInformation("Iniciando actualización PUT de categoría. ID: {CategoriaId}", categoriaId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categoriaUpdateDto == null || categoriaId != categoriaUpdateDto.Id)
                {
                    return BadRequest("La información de la categoría no es válida.");
                }

                var categoriaExistente = await _service.GetCategoriaByIdAsync(categoriaId);

                if (categoriaExistente == null)
                {
                    return NotFound($"No se encontro la categoria con ID {categoriaId}");
                }

                var resultado = await _service.UpdateAsync(categoriaId, categoriaUpdateDto);

                if (resultado == null)  
                {
                    return StatusCode(500, new { message = $"Algo salió mal actualizando el registro {categoriaUpdateDto.nombre}" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crítico actualizando categoría. ID: {CategoriaId}", categoriaId);
                return StatusCode(500, new
                {
                    Message = "Error interno del servidor",
                });
            }
        }
    }
}




