using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ForumContext context) : base(context)
        {
        }
    }
}