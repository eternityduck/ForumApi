using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ForumContext _context;
        public PostRepository(ForumContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}