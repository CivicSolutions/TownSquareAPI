using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.Post;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostController : ControllerBase
{
    private readonly PostService _postService;
    private readonly IMapper _mapper;

    public PostController(PostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(int communityId, CancellationToken cancellationToken)
    {
        var posts = await _postService.GetAll(communityId, cancellationToken);
        return Ok(posts);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int communityId, int id, CancellationToken cancellationToken)
    {
        var post = await _postService.GetById(communityId, id, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {id} not found.");
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostRequestDTO request, CancellationToken cancellationToken)
    {
        Post post = _mapper.Map<Post>(request);
        Post createdPost = await _postService.Create(post, cancellationToken);
        PostResponseDTO postDto = _mapper.Map<PostResponseDTO>(createdPost);
        return CreatedAtAction(nameof(GetById), new { id = postDto.Id }, postDto);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int communityId, int postId, [FromBody] PostRequestDTO request, CancellationToken cancellationToken)
    {
        Post post = _mapper.Map<Post>(request);
        Post? updatedPost = await _postService.Update(communityId, postId, post, cancellationToken);

        if (updatedPost == null)
        {
            return NotFound($"Post with ID {postId} not found.");
        }

        PostResponseDTO postDto = _mapper.Map<PostResponseDTO>(updatedPost);
        return CreatedAtAction(nameof(GetById), new { id = postDto.Id }, postDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int communityId, int postId, CancellationToken cancellationToken)
    {
        bool deleted = await _postService.Delete(communityId, postId, cancellationToken);

        if (!deleted)
        {
            return NotFound($"Post with ID {postId} not found.");
        }

        return NoContent();
    }
}