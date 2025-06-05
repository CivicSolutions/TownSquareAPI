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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var posts = await _postService.GetAll(cancellationToken);

        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(posts);
        return Ok(postResponseDTO);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var post = await _postService.GetById(id, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {id} not found.");
        }

        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(post);
        return Ok(postResponseDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostRequestDTO request, CancellationToken cancellationToken)
    {
        Post post = _mapper.Map<Post>(request);
        Post createdPost = await _postService.Create(post, cancellationToken);
        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(createdPost);
        return CreatedAtAction(nameof(GetById), new { id = postResponseDTO.Id }, postResponseDTO);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int postId, [FromBody] PostRequestDTO request, CancellationToken cancellationToken)
    {
        Post post = _mapper.Map<Post>(request);
        Post? updatedPost = await _postService.Update(postId, post, cancellationToken);

        if (updatedPost == null)
        {
            return NotFound($"Post with ID {postId} not found.");
        }

        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(updatedPost);
        return CreatedAtAction(nameof(GetById), new { id = postResponseDTO.Id }, postResponseDTO);
    }

    [HttpPost("Like")]
    public async Task<IActionResult> Like(int postId, CancellationToken cancellationToken)
    {
        Post? post = await _postService.IncrementLikeCount(postId, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {postId} not found.");
        }
        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(post);
        return Ok(postResponseDTO);
    }

    [HttpPost("Unlike")]
    public async Task<IActionResult> Unlike(int postId, CancellationToken cancellationToken)
    {
        Post? post = await _postService.DecrementLikeCount(postId, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {postId} not found.");
        }
        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(post);
        return Ok(postResponseDTO);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int postId, CancellationToken cancellationToken)
    {
        bool deleted = await _postService.Delete(postId, cancellationToken);

        if (!deleted)
        {
            return NotFound($"Post with ID {postId} not found.");
        }

        return NoContent();
    }
}