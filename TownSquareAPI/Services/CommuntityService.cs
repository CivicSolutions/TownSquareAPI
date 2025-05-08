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

        public Community? GetById(int id)
        {
            return _dbContext.Communities.FirstOrDefault(c => c.id == id);
        }
        public Community? GetByName(string name)
        {
            return _dbContext.Communities.FirstOrDefault(c => c.name == name);
        }
        public Community CreateCommunity(string name, string description, string location, bool isLicensed)
        {
            var community = new Community
            {
                name = name,
                description = description,
                location = location,
                isLicensed = isLicensed
            };

            _dbContext.Communities.Add(community);
            _dbContext.SaveChanges();

            return community;
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
        public Community? UpdateCommunity(int communityId, string name, string location, string description)
        {
            var community = _dbContext.Communities.FirstOrDefault(c => c.id == communityId);

            if (community == null)
            {
                return null;
            }

            community.name = name;
            community.location = location;
            community.description = description;

            _dbContext.SaveChanges();
            return community;
        }
        public bool DeleteCommunity(int communityId)
        {
            var community = _dbContext.Communities.FirstOrDefault(c => c.id == communityId);

            if (community == null)
            {
                return false;
            }

            _dbContext.Communities.Remove(community);
            _dbContext.SaveChanges();

            return true;
        }
        internal object CreateCommunity(string name, string description, string location, string isLicensed)
        {
            throw new NotImplementedException();
        }
    }
}