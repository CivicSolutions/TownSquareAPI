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
            return _dbContext.Community.ToList();
        }

        public Community? GetById(int id)
        {
            return _dbContext.Community.FirstOrDefault(c => c.Id == id);
        }
        public Community? GetByName(string name)
        {
            return _dbContext.Community.FirstOrDefault(c => c.Name == name);
        }
        public Community CreateCommunity(string name, string description, string location, bool isLicensed)
        {
            var community = new Community
            {
                Name = name,
                Description = description,
                Location = location,
                IsLicensed = isLicensed
            };

            _dbContext.Community.Add(community);
            _dbContext.SaveChanges();

            return community;
        }

        public void InsertMembershipRequest(int userId, int communityId)
        {
            var request = new UserCommunityRequest
            {
                UserId = userId,
                CommunityId = communityId
            };
            _dbContext.Add(request);
            _dbContext.SaveChanges();
        }
        public Community? UpdateCommunity(int communityId, string name, string location, string description)
        {
            var community = _dbContext.Community.FirstOrDefault(c => c.Id == communityId);

            if (community == null)
            {
                return null;
            }

            community.Name = name;
            community.Location = location;
            community.Description = description;

            _dbContext.SaveChanges();
            return community;
        }
        public bool DeleteCommunity(int communityId)
        {
            var community = _dbContext.Community.FirstOrDefault(c => c.Id == communityId);

            if (community == null)
            {
                return false;
            }

            _dbContext.Community.Remove(community);
            _dbContext.SaveChanges();

            return true;
        }
        internal object CreateCommunity(string name, string description, string location, string isLicensed)
        {
            throw new NotImplementedException();
        }
    }
}