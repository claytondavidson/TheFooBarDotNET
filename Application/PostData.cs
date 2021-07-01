using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
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

        public async Task<List<PostModel>> GetPosts()
        {
            var sql = "SELECT * FROM get_posts()";

            return await _db.GetAll<PostModel, dynamic>(sql, new { });
        }

        public async Task<PostModel> GetPost(int id)
        {
            var sql = "SELECT * FROM get_post_by_id(@id)";

            return await _db.Get<PostModel, dynamic>(sql, new { id });
        }

        public Task CreatePost(string title, string body, bool published)
        {
           var sql = "SELECT * FROM insert_post(@title, @body, @published)";

           return _db.Insert(sql, new { title, body, published });
        }

        public Task DeletePost(int id)
        {
            var sql = "SELECT * FROM delete_post(@id)";

            return _db.Delete(sql, new {id});
        }
    }
}