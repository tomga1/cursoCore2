using cursoCore2API.Data;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace cursoCore2API.Repository
{
    public class ProductoRepository : IproductoRepository
    {
        private readonly StoreContext _context;

        public ProductoRepository(StoreContext context)
        {
            _context = context; 
        }

        public bool ActualizarProducto(Producto producto)
        {
            
            _context.productos.Update(producto);
            
            return Guardar();
        }

        public bool CrearProducto(Producto producto)
        {
            _context.productos.Add(producto);
            return Guardar();
        }
        


        public bool BorrarProducto(Producto producto)
        {
            _context.productos.Remove(producto);
            return Guardar();
        }

        public bool ExisteProducto(int id)
        {
            return _context.productos.Any(c => c.idProducto == id); 
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _context.productos.Any(c => c.nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;   
        }

        public Producto GetProducto(int productoId)
        {
            return _context.productos.FirstOrDefault(c => c.idProducto == productoId);
        }

        //V1

        //public ICollection<Producto> GetProductos()
        //{
        //    return _context.productos.OrderBy(c => c.nombre).ToList();  
        //}

        public ICollection<Producto> GetProductos(int pageNumber, int pageSize)
        {
            return _context.productos.OrderBy(c => c.nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalProductos()
        {
            return _context.productos.Count();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;  
        }

        public ICollection<Producto> GetProductosEnCategoria(int catId)
        {
            return _context.productos.Include(pro => pro.Categoria).Where(pro => pro.categoriaId == catId).ToList();   
        }

        public IEnumerable<Producto> BuscarProducto(string nombre)
        {
            IQueryable<Producto> query = _context.productos;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.nombre.Contains(nombre) || e.descripcion.Contains(nombre));
            }
            return query.ToList();
        }
    }
}
