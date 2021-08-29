using System;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get;}
        ICommentRepository Comments { get;}
        IRepository<User> Users { get;}
        Task<int> SaveAsync();
    }
}