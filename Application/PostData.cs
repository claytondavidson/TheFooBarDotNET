using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Persistence;

namespace Application
{
    public class PostData : IPostData
    {
        private readonly IDataAccess _db;

        public PostData(IDataAccess db)
        {
            _db = db;
        }

        public Task<List<Post>> GetPosts()
        {
            var sql = "SELECT * FROM posts";

            return _db.LoadData<Post, dynamic>(sql, new { });
        }
        
    }
}