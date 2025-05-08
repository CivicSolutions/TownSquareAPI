using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services;

public class HelpPostService
{
    private readonly ApplicationDbContext _dbContext;

    public HelpPostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<HelpPost> GetHelpPosts()
    {
        return _dbContext.HelpPost.ToList();
    }

    public void AddHelpPost(HelpPost helpPost)
    {
        _dbContext.HelpPost.Add(helpPost);
        _dbContext.SaveChanges();
    }
}