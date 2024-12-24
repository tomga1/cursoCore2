using cursoCore2API.Models;

namespace cursoCore2API.Repository.IRepository
{
    public interface IproductoRepository
    {
        ICollection<Producto> GetProductos(int pageNumber, int pageSize);
        ICollection<Producto> GetProductosEnCategoria(int catId);
        int GetTotalProductos();
        IEnumerable<Producto> BuscarProducto(string nombre);

        Producto GetProducto(int productoId);
        bool ExisteProducto(int id);
        bool ExisteProducto(string nombre);

        bool CrearProducto(Producto producto);
        bool ActualizarProducto(Producto producto);
        bool BorrarProducto(Producto producto);

        bool Guardar();
    }
}
