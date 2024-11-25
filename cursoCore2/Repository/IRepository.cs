namespace cursoCore2API.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task Add(TEntity entity);
        void Updater(TEntity entity);   
        void Delete(TEntity entity);
        Task Save();
    }
}
