using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task<List<T>> GetAllAsync();
        T GetById(int id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        Task RemoveAsync(T item);
        void Update(T item);
        bool Any(Func<T, bool> predicate);
        Task RemoveByIdAsync(int id);
    }
    
}