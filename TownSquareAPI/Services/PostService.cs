using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services;

public class PostService
{
    private readonly ApplicationDbContext _dbContext;

    public PostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Post>> GetAll(int communityID, CancellationToken cancellationToken)
    {
        return await _dbContext.Post.Where(p => p.CommunityId == communityID).ToListAsync(cancellationToken);
    }

    public async Task<Post?> GetById(int communityId, int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Post.FirstOrDefaultAsync(p => p.CommunityId == communityId && p.Id == id, cancellationToken);
    }

    public async Task<Post> Create(Post post, CancellationToken cancellationToken)
    {
        _dbContext.Post.Add(post);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return post;
    }

    public async Task<Post?> Update(int communityId, int postId, Post post, CancellationToken cancellationToken)
    {
        Post? postToUpdate = await _dbContext.Post.FirstOrDefaultAsync(p => p.CommunityId == communityId && p.Id == postId, cancellationToken);

        if (postToUpdate == null)
        {
            return null;
        }

        _dbContext.Post.Update(post);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return post;
    }

    public async Task<bool> Delete(int communityId, int postId, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Post.FirstOrDefaultAsync(p => p.CommunityId == communityId && p.Id == postId, cancellationToken);

        if (post == null)
        {
            return false;
        }

        _dbContext.Post.Remove(post);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}