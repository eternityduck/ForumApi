using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumContext _context;
        public IPostRepository Posts { get; }
        public ICommentRepository Comments { get; }
        public IUserRepository Users { get; }
        

        public UnitOfWork(ForumContext context)
        {
            _context = context;
            Posts = new PostRepository(_context);
            Comments = new CommentRepository(_context);
            Users = new UserRepository(_context);
        }
        
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}