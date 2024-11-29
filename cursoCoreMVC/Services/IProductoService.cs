using cursoCoreMVC.Models;

namespace cursoCoreMVC.Services
{
    public interface IproductoService
    {
        Task<List<Productos>> Lista();  // Método para obtener la lista de productos
        Task<Productos> ObtenerPorId(int id);  // Método para obtener un producto por ID
        Task<bool> Eliminar(int idProducto);
    }
}
