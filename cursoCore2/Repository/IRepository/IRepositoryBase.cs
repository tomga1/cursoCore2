using System.Linq.Expressions;
using System.Collections.Generic;

namespace cursoCore2API.Repository.IRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Remove(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        bool SaveChanges();
    }
}
