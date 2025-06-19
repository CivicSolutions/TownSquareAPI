using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services;

public class CommunityService
{
    private readonly ApplicationDbContext _dbContext;

    public CommunityService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Community>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Community.ToListAsync(cancellationToken);
    }

    public async Task<Community?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Community.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Community?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Community.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<Community> Create(Community community, CancellationToken cancellationToken)
    {
        _dbContext.Community.Add(community);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return community;
    }

    public async Task<Community?> Update(int communityId, Community community, CancellationToken cancellationToken)
    {
        Community? communityToUpdate = await _dbContext.Community.FirstOrDefaultAsync(c => c.Id == communityId, cancellationToken);

        if (communityToUpdate == null)
        {
            return null;
        }

        _dbContext.Community.Update(community);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return community;
    }

    public async Task<bool> Delete(int communityId, CancellationToken cancellationToken)
    {
        var community = await _dbContext.Community.FirstOrDefaultAsync(c => c.Id == communityId, cancellationToken);

        if (community == null)
        {
            return false;
        }

        _dbContext.Community.Remove(community);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task CreateMembershipRequest(UserCommunity userCommunity, CancellationToken cancellationToken)
    {
        _dbContext.UserCommunity.Add(userCommunity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> MembershipRequestExists(string userId, int communityId, CancellationToken cancellationToken)
    {
        return await _dbContext.UserCommunity.AnyAsync(uc => uc.UserId == userId && uc.CommunityId == communityId, cancellationToken);
    }
}