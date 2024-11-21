using cursoCore2.Models;
using cursoCore2API.Models;
using Microsoft.Identity.Client;
namespace cursoCore2API.Repositories
{
    public class ProductoRepository
    {
        private readonly StoreContext _context;

        public ProductoRepository(StoreContext context)
        {
            _context = context;
        }

        public List<Producto> Get()
        {
            return _context.productos.ToList();   
        }

        public List<Producto> Get(string nombre)
        {
            return _context.productos.Where(e => e.nombre.Contains(nombre)).ToList();
        }


        public Producto? Get(int id)
        {
            return _context.productos.FirstOrDefault(e => e.idProducto == id);
        }


        public Producto Remove(int id)
        {
            var producto = _context.productos.FirstOrDefault(e => e.idProducto == id);

            if (producto == null) 
            {
                return null;
            }
            _context.productos.Remove(producto);
            _context.SaveChanges();
            return producto;
        }

        public int Add(Producto producto)
        {
            _context.productos.Add(producto);   
            _context.SaveChanges();
            return producto.idProducto;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
