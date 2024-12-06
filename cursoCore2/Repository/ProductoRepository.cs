using cursoCore2API.Data;
using cursoCore2API.Models;
using cursoCore2API.Repository.IRepository;

namespace cursoCore2API.Repository
{
    public class ProductoRepository : ICategoriaRepository
    {
        private readonly StoreContext _context;

        public ProductoRepository(StoreContext context)
        {
            _context = context;
        }



        public bool CrearCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            _context.categoria.Add(categoria);
            return Guardar();
        }
        public bool ActualizarCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            var categoriaExistente = _context.categoria.Find(categoria.Id);

            if (categoriaExistente != null)
            {
                _context.Entry(categoriaExistente).CurrentValues.SetValues(categoria);

            }
            else
            {
                _context.categoria.Update(categoria);
            }
            _context.categoria.Update(categoria);
            return Guardar();
        }


        public bool BorrarCategoria(Categoria categoria)
        {
            _context.categoria.Remove(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(int id)
        {
            return _context.categoria.Any(c => c.Id == id);
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _context.categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public Categoria GetCategoria(int categoriaId)
        {
            return _context.categoria.FirstOrDefault(c => c.Id == categoriaId);
        }


        public ICollection<Categoria> GetCategorias()
        {
            return _context.categoria.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
