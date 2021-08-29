using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ForumContext _context;
        public CommentRepository(ForumContext context) : base(context)
        {
            _context = context;
        }

      

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}