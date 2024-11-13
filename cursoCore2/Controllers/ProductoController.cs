using cursoCore2.Data;
using cursoCore2API.Repositories;
using cursoCore2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cursoCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepository _repository;

        public ProductoController(ProductoRepository repository)
        {
            _repository = repository;
        }

        //private readonly AplicationDbContext _context;
        
        //public ProductoController(AplicationDbContext context)
        //{
        //    _context = context; 
        //}


        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaProductos = _repository.Get();
                return Ok(listaProductos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("{titleForSearch}")]
        public IActionResult Get(string titleForSearch)
        {
            return Ok(_repository.Get(titleForSearch));
        }
        

        //POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            try
            {
                _repository.Add(producto);
                return Ok(producto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producto producto)
        {
            try
            {
                var productoExistente = _repository.Get(id);

                if (productoExistente == null)
                {
                    return NotFound(new { message = "Producto no encontrado." });
                }

                productoExistente.nombre = producto.nombre;
                productoExistente.descripcion = producto.descripcion;
                productoExistente.stock = producto.stock;
                productoExistente.precio = producto.precio;
                productoExistente.imagen = producto.imagen;

                await _repository.SaveChangesAsync();

                return Ok(new { message = "El producto fue actualizado con éxito!" });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        //// DELETE api/<ProductoController>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var productoEliminado = _repository.Remove(id); 

        //        if (productoEliminado == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(new { message = "El producto fue eliminado con éxito!", producto = productoEliminado });

        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
