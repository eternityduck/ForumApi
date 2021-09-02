using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly ForumContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ForumContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.AsNoTracking().ToListAsync();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        public async Task AddAsync(T item)
        {
            
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task RemoveAsync(T item)
        {
            _dbSet.Remove(item);
            return _context.SaveChangesAsync();
        }

        public bool Any(Func<T, bool> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public Task RemoveByIdAsync(int id)
        {
            _dbSet.Remove(GetById(id));
            return _context.SaveChangesAsync();
        }
    }
}