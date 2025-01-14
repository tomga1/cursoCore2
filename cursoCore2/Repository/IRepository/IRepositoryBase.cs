using System.Linq.Expressions;
using System.Collections.Generic;

namespace cursoCore2API.Repository.IRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<bool> AddAsync(T entity); 
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(); 
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
