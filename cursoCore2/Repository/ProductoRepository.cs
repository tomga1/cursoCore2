using cursoCore2API.Data;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace cursoCore2API.Repository
{
    public class ProductoRepository : RepositoryBase<Producto>, IproductoRepository
    {
        public ProductoRepository(StoreContext context) : base(context)
        {
        }
       
        public ICollection<Producto> GetProductos(int pageNumber, int pageSize)
        {
            return _dbSet.OrderBy(c => c.nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Producto GetProducto(int productoId)
        {
            return _dbSet.FirstOrDefault(c => c.idProducto == productoId);
        }

        public bool ExisteProducto(int id)
        {
            return _dbSet.Any(c => c.idProducto == id); 
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _dbSet.Any(c => c.nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;   
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
