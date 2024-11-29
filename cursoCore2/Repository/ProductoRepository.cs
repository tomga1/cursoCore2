using cursoCore2.Models;
using cursoCore2API.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Repository
{
    public class ProductoRepository : IRepository<Producto>
    {
        private StoreContext _context; 

        public ProductoRepository(StoreContext context)
        {
            _context = context; 
        }


        public async Task<IEnumerable<Producto>> Get()
            => await _context.productos.ToListAsync();

        public async Task<Producto> GetById(int id)
            => await _context.productos.FindAsync(id);

        public async Task Add(Producto producto)
            => await _context.productos.AddAsync(producto);

        public void Update(Producto producto)
        {
            _context.productos.Attach(producto);
            _context.productos.Entry(producto).State = EntityState.Modified;
        }


        public async void Delete(Producto producto)
            => _context.productos.Remove(producto);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<Producto> Search(Func<Producto, bool> filter) =>
            _context.productos.Where(filter).ToList();
    }
}
