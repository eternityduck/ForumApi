using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ForumContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(ForumContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return DbSet.AsNoTracking().ToListAsync();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }
        
        public async Task AddAsync(T item)
        {
             await DbSet.AddAsync(item);
        }

        public void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public Task RemoveAsync(T item)
        {
            DbSet.Remove(item);
            return Context.SaveChangesAsync();
        }

        public bool Any(Func<T, bool> predicate)
        {
            return DbSet.Any(predicate);
        }

        public Task RemoveByIdAsync(int id)
        {
            DbSet.Remove(GetById(id));
            return Context.SaveChangesAsync();
        }
    }
}