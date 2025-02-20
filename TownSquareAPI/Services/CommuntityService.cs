using TownSquareAPI.Data;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services
{
    public class CommunityService
    {
        private readonly ApplicationDbContext _dbContext;

        public CommunityService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Community> GetAllCommunities()
        {
            return _dbContext.Communities.ToList();
        }

        public void InsertMembershipRequest(int userId, int communityId)
        {
            var request = new UserCommunityRequest
            {
                user_id = userId,
                community_id = communityId
            };
            _dbContext.Add(request);
            _dbContext.SaveChanges();
        }
    }
}