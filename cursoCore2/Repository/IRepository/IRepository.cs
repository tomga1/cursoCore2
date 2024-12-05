namespace cursoCore2API.Repository.IRepository
{
    public interface IRepository<TEntity>
    {
        ICollection<TEntity> GetProductoEnCategoria(int catId);
        IEnumerable<TEntity> BuscarProducto(string nombre);

        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Save();

        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    }
}
