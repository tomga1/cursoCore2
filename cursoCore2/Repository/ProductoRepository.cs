using cursoCore2.Models;

namespace cursoCore2API.Repository
{
    public class ProductoRepository : IRepository<Producto>
    {

        public Task<IEnumerable<Producto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Producto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Add(Producto entity)
        {
            throw new NotImplementedException();
        }

        public void Updater(Producto entity)
        {
            throw new NotImplementedException();
        }
        
        
        public void Delete(Producto entity)
        {
            throw new NotImplementedException();
        }
        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
