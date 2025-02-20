using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Post> GetPosts(int isNews)
        {
            return _dbContext.Set<Post>().Where(p => p.isnews == isNews).ToList();
        }

        public void CreatePost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }
    }
}