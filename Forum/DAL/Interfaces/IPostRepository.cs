using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetByIdAsync(int id);
    }
}