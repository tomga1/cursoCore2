using cursoCore2API.Models;

namespace cursoCore2API.Repository.IRepository
{
    public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int categoriaId);
        bool ExisteCategoria(int id);
        bool ExisteCategoria(string nombre);


        //bool CrearCategoria(Categoria categoria);
        //bool ActualizarCategoria(Categoria categoria);
        //bool BorrarCategoria(Categoria categoria);
    }
}
