﻿using cursoCore2API.DTOs;
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



namespace cursoCore2API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/categorias")]
    [ApiController]
    [AllowAnonymous]
    public class CategoriasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _service;
        private readonly IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto> _serviceBase;
        private readonly IMessageService _messageService;

        public CategoriasController(IMapper mapper, ICategoriaService service, IMessageService messageService, IServiceBase<Categoria, CategoriaInsertDto, CategoriaUpdateDto> serviceBase)
        {
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
           
                var itemCategoria = await _service.GetByIdAsync(categoriaId);

                if (itemCategoria == null)
                {
                var message = _messageService.GetMessage("CategoriaNoEncontrada");
                    return NotFound(new { message});
                }

                var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria); 

                return Ok(itemCategoriaDto);
            
        }

        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaInsertDto categoriaInsertDto)
        {
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


        [Authorize(Roles = "admin")]
        [HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaUpdateDto categoriaUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoriaUpdateDto == null || categoriaId != categoriaUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            // Verificamos si la categoría existe
            var categoriaExistente = await _service.GetByIdAsync(categoriaId);

            if (categoriaExistente == null)  
            {
                return NotFound($"No se encontró la categoría con ID {categoriaId}");
            }

            // Llamamos al servicio para actualizar la entidad usando el DTO
            var actualizado = await _service.UpdateAsync(categoriaId, categoriaUpdateDto);

            if (actualizado == null)
            {
                ModelState.AddModelError("", "Algo salió mal actualizando el registro");
                return StatusCode(500, ModelState);
            }

            return NoContent(); // Si todo fue bien, devolvemos NoContent
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


        //[Authorize(Roles = "admin")]
        [HttpPut("{categoriaId:int}", Name = "ActualizarPutCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<IActionResult> ActualizarPutCategoria(int categoriaId, [FromBody] CategoriaUpdateDto categoriaUpdateDto)
        {
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


    }
}




