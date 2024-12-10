//using cursoCore2.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.AspNetCore.Authorization;
//using cursoCore2API.Models;
//using Microsoft.AspNetCore.Identity;
//using cursoCore2API.DTOs;
//using FluentValidation;
//using cursoCore2API.Services;


//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace cursoCore2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class ProductoController : ControllerBase
//    {
//        private IValidator<ProductoInsertDto> _productoInsertValidator;
//        private IValidator<ProductoUpdateDto> _productoUpdateValidator;
//        private ICommonService<ProductoDto, ProductoInsertDto, ProductoUpdateDto> _productoService; 


//        public ProductoController(IValidator<ProductoInsertDto> productoInsertValidator,
//            IValidator<ProductoUpdateDto> productoUpdateValidator,
//            [FromKeyedServices("productoService")]ICommonService<ProductoDto, ProductoInsertDto, ProductoUpdateDto> productoService)
//        {
//            _productoInsertValidator = productoInsertValidator;
//            _productoUpdateValidator = productoUpdateValidator; 
//            _productoService = productoService; 
//        }

//        [HttpGet]
//        public async Task<IEnumerable<ProductoDto>> Get() =>
//            await _productoService.Get();


//        [HttpGet("{id}")]
//        public async Task<ActionResult<ProductoDto>> GetById(int id)
//        {
//            var productoDto = await _productoService.GetById(id);

//            return productoDto == null ? NotFound() : Ok(productoDto);
//        }



//        [HttpPost]
//        public async Task<ActionResult<ProductoDto>> Add(ProductoInsertDto productoInsertDto)
//        {
//            var validationResult = await _productoInsertValidator.ValidateAsync(productoInsertDto);  

//            if(!validationResult.IsValid)
//            {
//                return BadRequest(validationResult.Errors);
//            }

//            if (!_productoService.Validate(productoInsertDto))
//            {
//                return BadRequest(_productoService.Errors); 
//            }


//            var productoDto = await _productoService.Add(productoInsertDto);

//            return CreatedAtAction(nameof(GetById), new {id = productoDto.idProducto}, productoDto);
//        }


//        [HttpPut("{id}")]
//        public async Task<ActionResult<ProductoDto>> Update(int id, ProductoUpdateDto productoUpdateDto)
//        {
//            var validationResult = await _productoUpdateValidator.ValidateAsync(productoUpdateDto);

//            if (!validationResult.IsValid)
//            {
//                return BadRequest(validationResult.Errors);
//            }
//            if (!_productoService.Validate(productoUpdateDto))
//            {
//                return BadRequest(_productoService.Errors);
//            }

//            var productoDto = await _productoService.Update(id, productoUpdateDto);
           

//            return productoDto == null ? NotFound() : Ok(productoDto);
//        }


//        [HttpDelete("{id}")]
//        public async Task<ActionResult<ProductoDto>> Delete(int id)
//        {
//            var productoDto = await _productoService.Delete(id);

//            return productoDto == null ? NotFound() : Ok(productoDto); 
//        }


//    }
//}
