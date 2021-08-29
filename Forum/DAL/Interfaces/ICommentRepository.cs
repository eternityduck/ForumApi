using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> GetByIdAsync(int id);
    }
}