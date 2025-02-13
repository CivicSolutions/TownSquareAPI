using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet("GetPosts/{isNews}")]
        public IActionResult GetPosts(bool isNews)
        {
            var posts = _postService.GetPosts(isNews);
            return Ok(posts);
        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost([FromBody] CreatePostDto postDto)
        {
            var post = new Post
            {
                Content = postDto.Content,
                UserId = postDto.UserId,
                IsNews = postDto.IsNews,
                CreatedAt = DateTime.UtcNow,
                CommunityId = postDto.CommunityId
            };

            _postService.CreatePost(post);

            return Ok("Beitrag erstellt.");
        }
    }
}