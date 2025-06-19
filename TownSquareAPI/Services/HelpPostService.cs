using Microsoft.EntityFrameworkCore;
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

    public async Task<List<HelpPost>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.HelpPost.ToListAsync(cancellationToken);
    }

    public async Task<HelpPost?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.HelpPost.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public async Task<HelpPost> Create(HelpPost helpPost, CancellationToken cancellationToken)
    {
        _dbContext.HelpPost.Add(helpPost);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return helpPost;
    }

    public async Task<HelpPost?> Update(int id, HelpPost helpPost, CancellationToken cancellationToken)
    {
        HelpPost? helpPostToUpdate = await _dbContext.HelpPost.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (helpPostToUpdate == null)
        {
            return null;
        }

        _dbContext.HelpPost.Update(helpPost);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return helpPost;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        HelpPost? helpPostToDelete = await _dbContext.HelpPost.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (helpPostToDelete == null)
        {
            return false;
        }

        _dbContext.HelpPost.Remove(helpPostToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}