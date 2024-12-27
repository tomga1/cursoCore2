using System.Collections;

namespace cursoCoreMVC.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable> GetAllAsync(string url);
        Task<IEnumerable> GetProductosEnCategoriaAsync(string url, int categoriaId);
        Task<IEnumerable> Buscar(string url, string nombre);
        Task<T> GetAsync(string url, int Id);
        Task<bool> CrearAsync(string url, T itemCrear);
        Task<bool> CrearProductoAsync(string url, T productoACrear);


        Task<bool> ActualizarAsync(string url, T itemActualizar);
        Task<bool> ActualizarProductoAsync(string url, T productoAActualizar);

        Task<bool> BorrarAsync(string url, int Id);


    }
}
