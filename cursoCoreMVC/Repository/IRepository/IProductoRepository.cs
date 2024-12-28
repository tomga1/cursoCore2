using cursoCoreMVC.Models;

namespace cursoCoreMVC.Repository.IRepository
{
    public interface IProductoRepository : IRepository<Productos>
    {
        Task<IEnumerable<Productos>> GetProductosAsync(string url);
    }
}
