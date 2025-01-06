using cursoCore2API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using cursoCore2API.Repository.IRepository;

namespace cursoCore2API.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly StoreContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(StoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual bool Add(T entity)
        {
            _dbSet.Add(entity);
            return SaveChanges();
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return SaveChanges();
        }

        public bool Remove(T entity)
        {
            _dbSet.Remove(entity);
            return SaveChanges();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
