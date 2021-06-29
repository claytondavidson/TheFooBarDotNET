using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Application
{
    public interface IPostData
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPost(int id);
    }
}