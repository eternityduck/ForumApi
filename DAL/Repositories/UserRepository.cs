using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ForumContext _context;
        public UserRepository(ForumContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id.ToString());
        }
    }
}