using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces
{
    public interface IPostData
    {
        Task<List<PostModel>> GetPosts();
        Task<PostModel> GetPost(int id);
        Task CreatePost(string title, string body, bool published);
        Task DeletePost(int id);
    }
}