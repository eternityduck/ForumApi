using System.Collections.Generic;
using System.Threading.Tasks;

using DAL.Models;

namespace BLL.Interfaces
{
    public interface ITopicService : ICrud<Topic>
    {
        Task UpdateTopicTitle(int id, string title);
        
        IEnumerable<Post> GetFilteredPosts(int forumId, string modelSearchQuery);
        IEnumerable<User> GetUsers(int forumId);
        Task<IEnumerable<Post>> GetRecentPostsAsync(int id, int days);
    }
}