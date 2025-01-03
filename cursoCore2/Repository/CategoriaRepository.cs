using cursoCore2API.Data;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;

namespace cursoCore2API.Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(StoreContext context) : base(context)
        {
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _dbSet.OrderBy(c => c.categoria_nombre).ToList();
        }

        public Categoria GetCategoria(int categoriaId)
        {
            return _dbSet.FirstOrDefault(c => c.categoriaId == categoriaId);
        }

        public override bool Add(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            return base.Add(categoria); 
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
