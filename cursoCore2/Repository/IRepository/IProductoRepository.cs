using cursoCore2.Models;
using cursoCore2API.Models;

namespace cursoCore2API.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        ICollection<Producto> GetCategorias();
        ICollection<Producto> GetProductosEnCategoria(int catId);
        IEnumerable<Producto> BuscarPeliculas(string nombre);

        Producto GetCategoria(int productoId);
        bool ExisteCategoria(int id);
        bool ExisteCategoria(string nombre);

        bool CrearCategoria(Producto producto);
        bool ActualizarCategoria(Producto producto);
        bool BorrarCategoria(Producto producto);

        bool Guardar();
    }
}
