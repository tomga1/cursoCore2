using cursoCoreMVC.Models;

namespace cursoCoreMVC.Services
{
    public interface IService_API
    {
        Task<List<Productos>> Lista();
        Task<Productos> Obtener(int idProducto);
        Task<bool> Guardar(Productos objeto);
        Task<bool> Editar(Productos objeto);
        Task<bool> Eliminar(int idProducto);



    }
}
