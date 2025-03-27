using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs;
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
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var community = _communityService.GetByName(name);
            return Ok(community);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var community = _communityService.GetById(id);
            if (community == null)
            {
                return NotFound($"Community with ID {id} not found.");
            }
            return Ok(community);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateCommunityRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.name) || string.IsNullOrWhiteSpace(request.location))
            {
                return BadRequest("Name and location are required.");
            }

            var community = _communityService.CreateCommunity(request.name, request.description, request.location, request.isLicensed);
            return CreatedAtAction(nameof(GetById), new { id = community.Id }, community);
        }

        [HttpPut("Update/{communityId}")]
        public IActionResult UpdateCommunity(int communityId, [FromBody] UpdateCommunityRequest request)
        {
            var updatedCommunity = _communityService.UpdateCommunity(communityId, request.name, request.location, request.description);

            if (updatedCommunity == null)
            {
                return NotFound($"Community with ID {communityId} not found.");
            }

            return Ok(updatedCommunity);
        }

        [HttpDelete("Delete/{communityId}")]
        public IActionResult DeleteCommunity(int communityId)
        {
            bool isDeleted = _communityService.DeleteCommunity(communityId);

            if (!isDeleted)
            {
                return NotFound($"Community with ID {communityId} not found.");
            }

            return Ok($"Community with ID {communityId} has been deleted.");
        }


        [HttpPut("RequestMembership")]
        public IActionResult RequestMembership(int userId, int communityId)
        {
            _communityService.InsertMembershipRequest(userId, communityId);
            return Ok("Membership request submitted.");
        }
    }
}