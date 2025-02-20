using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunityController : ControllerBase
    {
        private readonly CommunityService _communityService;

        public CommunityController(CommunityService communityService)
        {
            _communityService = communityService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var communities = _communityService.GetAllCommunities();
            return Ok(communities);
        }

        [HttpPut("RequestMembership")]
        public IActionResult RequestMembership(int userId, int communityId)
        {
            _communityService.InsertMembershipRequest(userId, communityId);
            return Ok("Membership request submitted.");
        }
    }
}