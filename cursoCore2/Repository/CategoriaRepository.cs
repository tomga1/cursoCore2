using cursoCore2API.Data;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2API.Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(StoreContext context) : base(context)
        {
        }

        public async Task<ICollection<Categoria>> GetCategoriasAsync()
        {
            return await _dbSet.OrderBy(c => c.categoria_nombre).ToListAsync();
        }

        public async Task<Categoria> GetCategoriaByid(int categoriaId)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.categoriaId == categoriaId);
        }

        public bool ExisteCategoria(int id)
        {
            return _dbSet.Any(c => c.categoriaId == id); 
        }

        public bool ExisteCategoria(string nombre)
        {
            return _dbSet.Any(c => c.categoria_nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }
    }
}
