using cursoCore2API.Repositories;
using cursoCore2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using cursoCore2API.Models;
using Microsoft.AspNetCore.Identity;
using cursoCore2API.DTOs;
using FluentValidation;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cursoCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<ProductoInsertDto> _productoInsertValidator;
        private IValidator<ProductoUpdateDto> _productoUpdateValidator;


        public ProductoController(StoreContext context,
            IValidator<ProductoInsertDto> productoInsertValidator,
            IValidator<ProductoUpdateDto> productoUpdateValidator)
        {
            _context = context;
            _productoInsertValidator = productoInsertValidator;
            _productoUpdateValidator = productoUpdateValidator; 
        }

        //private readonly AplicationDbContext _context;

        //public ProductoController(AplicationDbContext context)
        //{
        //    _context = context; 
        //}


        [HttpGet]
        public async Task<IEnumerable<ProductoDto>> Get() =>
            await _context.productos.Select(b => new ProductoDto
            {
                idProducto = b.idProducto,
                nombre = b.nombre,
                stock = b.stock,
                descripcion = b.descripcion,
                precio = b.precio,
                imagen = b.imagen,
            }).ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetById(int id)
        {
            var producto = await _context.productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();  
            }

            var productoDto = new ProductoDto
            {
                idProducto = producto.idProducto,
                nombre = producto.nombre,
                stock = producto.stock,
                descripcion = producto.descripcion,
                precio = producto.precio,
                imagen = producto.imagen

            };
            return Ok(productoDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Add(ProductoInsertDto productoInsertDto)
        {
            var validationResult = await _productoInsertValidator.ValidateAsync(productoInsertDto);  

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var producto = new Producto()
            {
                nombre = productoInsertDto.nombre,
                stock = productoInsertDto.stock,
                descripcion = productoInsertDto.descripcion,
                precio = productoInsertDto.precio,
                imagen = productoInsertDto.imagen
            };

            await _context.productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            var productoDto = new ProductoDto
            {
                idProducto = producto.idProducto,
                nombre = producto.nombre,
                stock = producto.stock,
                descripcion = producto.descripcion,
                precio = producto.precio,
                imagen = producto.imagen 
            };

            return CreatedAtAction(nameof(GetById), new {id = producto.idProducto}, productoDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDto>> Update(int id, ProductoUpdateDto productoUpdateDto)
        {
            var validationResult = await _productoUpdateValidator.ValidateAsync(productoUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var producto = await _context.productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.nombre = productoUpdateDto.nombre;
            producto.stock = productoUpdateDto.stock;
            producto.descripcion = productoUpdateDto.descripcion;
            producto.precio = productoUpdateDto.precio;
            producto.imagen = productoUpdateDto.imagen;
            producto.idMarca = producto.idMarca;

            await _context.SaveChangesAsync();

            var productoDto = new ProductoDto
            {
                idProducto = producto.idProducto,
                nombre = producto.nombre,
                stock = producto.stock,
                descripcion = producto.descripcion,
                precio = producto.precio,
                imagen = producto.imagen
            };

            return Ok(productoDto);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _context.productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok();
        }






        //    // GET api/<ProductoController>/5
        //    [HttpGet("{titleForSearch}")]
        //    public IActionResult Get(string titleForSearch)
        //    {
        //        return Ok(_repository.Get(titleForSearch));
        //    }


        //    //POST api/<ProductoController>
        //    [HttpPost]
        //    public async Task<IActionResult> Post([FromBody] Producto producto)
        //    {
        //        try
        //        {
        //            _repository.Add(producto);
        //            return Ok(producto);
        //        }
        //        catch (Exception ex)
        //        {

        //            return BadRequest(ex.Message);
        //        }
        //    }

        //    // PUT api/<ProductoController>/5
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> Put(int id, [FromBody] Producto producto)
        //    {
        //        try
        //        {
        //            var productoExistente = _repository.Get(id);

        //            if (productoExistente == null)
        //            {
        //                return NotFound(new { message = "Producto no encontrado." });
        //            }

        //            productoExistente.nombre = producto.nombre;
        //            productoExistente.descripcion = producto.descripcion;
        //            productoExistente.stock = producto.stock;
        //            productoExistente.precio = producto.precio;
        //            productoExistente.imagen = producto.imagen;

        //            await _repository.SaveChangesAsync();

        //            return Ok(new { message = "El producto fue actualizado con éxito!" });

        //        }
        //        catch (Exception ex)
        //        {

        //            return BadRequest(ex.Message);
        //        }
        //    }




        //    // DELETE api/<ProductoController>/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> Delete(int id)
        //    {
        //        try
        //        {
        //            var productoEliminado = _repository.Remove(id);

        //            if (productoEliminado == null)
        //            {
        //                return NotFound();
        //            }

        //            return Ok(new { message = "El producto fue eliminado con éxito!", producto = productoEliminado });

        //        }
        //        catch (Exception ex)
        //        {

        //            return BadRequest(ex.Message);
        //        }
        //    }
    }
}
