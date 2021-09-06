using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ForumContext context) : base(context)
        { }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await Context.Posts.Include(x => x.Author)
                .Include(x => x.Comments).ThenInclude(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override Task<List<Post>> GetAllAsync()
        {
            var result = DbSet.AsNoTracking()
                .Include(x => x.Author)
                .Include(x => x.Comments).ThenInclude(x => x.Author)
                .ToListAsync();

            return result;
        }
    }
}