using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ForumContext context) : base(context)
        { }

        public async Task<User> GetByIdAsync(string id)
        {
            var result = await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}