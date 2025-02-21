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
        public IActionResult GetPosts(int isNews)
        {
            var posts = _postService.GetPosts(isNews);
            return Ok(posts);
        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost([FromBody] CreatePostDto postDto)
        {
            var post = new Post
            {
                content = postDto.content,
                user_id = postDto.user_id,
                isnews = postDto.isnews,
                posttime = DateTime.UtcNow,
                community_id = postDto.community_id
            };

            _postService.CreatePost(post);

            return Ok("Post created.");
        }
    }
}