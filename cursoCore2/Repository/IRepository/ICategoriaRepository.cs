using cursoCore2API.Models;

namespace cursoCore2API.Repository.IRepository
{
    public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<ICollection<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaByid(int categoriaId);
        bool ExisteCategoria(int id);
        Task<bool> ExisteCategoria(string nombre);


        //bool CrearCategoria(Categoria categoria);
        //bool ActualizarCategoria(Categoria categoria);
        //bool BorrarCategoria(Categoria categoria);
    }
}
