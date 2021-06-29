using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Persistence.Interfaces;

namespace Application
{
    public class PostData : IPostData
    {
        private readonly IDataAccess _db;

        public PostData(IDataAccess db)
        {
            _db = db;
        }

        public async Task<List<Post>> GetPosts()
        {
            var sql = "SELECT * FROM get_posts()";

            return await _db.GetAll<Post, dynamic>(sql, new { });
        }

        public async Task<Post> GetPost(int id)
        {
            var sql = "SELECT * FROM get_post_by_id(@id)";

            return await _db.Get<Post, dynamic>(sql, new { id });
        }
    }
}