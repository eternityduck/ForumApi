using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetByIdAsync(int id);
    }
}