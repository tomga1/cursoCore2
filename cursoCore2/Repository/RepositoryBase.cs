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

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true; 
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync();
            
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); 
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
