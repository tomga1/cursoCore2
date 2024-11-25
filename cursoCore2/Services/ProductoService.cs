using cursoCore2.Models;
using cursoCore2API.DTOs;
using cursoCore2API.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Services
{
    public class ProductoService : ICommonService<ProductoDto, ProductoInsertDto, ProductoUpdateDto>
    {
        private StoreContext _context; 

        public ProductoService(StoreContext context)
        {
            _context = context;
        }


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

        public async Task<ProductoDto> GetById(int id)
        {
            var producto = await _context.productos.FindAsync(id);  

            if(producto != null)
            {
                var productoDto = new ProductoDto
                {
                    idProducto = producto.idProducto,
                    nombre = producto.nombre,
                    stock = producto.stock,
                    descripcion = producto.descripcion,
                    precio = producto.precio,
                    imagen = producto.imagen,
                };

                return productoDto;
            }

            return null;
        }

        public async Task<ProductoDto> Add(ProductoInsertDto productoInsertDto)
        {
            var producto = new Producto()
            {
                nombre = productoInsertDto.nombre,
                stock = productoInsertDto.stock,
                descripcion = productoInsertDto.descripcion,
                precio = productoInsertDto.precio,
                imagen = productoInsertDto.imagen,
                idMarca = productoInsertDto.idMarca
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
                imagen = producto.imagen,
            };

            return productoDto;
        }



        public async Task<ProductoDto> Update(int id, ProductoUpdateDto productoUpdateDto)
        {
            var producto = await _context.productos.FindAsync(id);

            if (producto != null)
            {
                producto.nombre = productoUpdateDto.nombre;
                producto.stock = productoUpdateDto.stock;
                producto.descripcion = productoUpdateDto.descripcion;
                producto.precio = productoUpdateDto.precio;
                producto.imagen = productoUpdateDto.imagen;

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

                return productoDto;
            }

            return null;
        }





        public async Task<ProductoDto> Delete(int id)
        {
            var producto = await _context.productos.FindAsync(id);

            if (producto != null)
            {
                var productoDto = new ProductoDto
                {
                    idProducto = producto.idProducto,
                    nombre = producto.nombre,
                    stock = producto.stock,
                    descripcion = producto.descripcion,
                    precio = producto.precio,
                    imagen = producto.imagen
                };

                _context.productos.Remove(producto);

                await _context.SaveChangesAsync();

                

                return productoDto;
            }

            return null;

        }

        

        

        
    }
}
