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

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(string userId, CancellationToken cancellationToken)
    {
        var posts = await _postService.GetAll(cancellationToken);

        List<PostResponseDTO> postResponseDTOs = _mapper.Map<List<PostResponseDTO>>(posts);
        foreach (var post in postResponseDTOs)
        {
            post.IsLikedByCurrentUser = await _postService.IsPostLikedByUser(post.Id, userId, cancellationToken);
        }
        return Ok(postResponseDTOs);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id, string userId, CancellationToken cancellationToken)
    {
        var post = await _postService.GetById(id, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {id} not found.");
        }

        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(post);
        postResponseDTO.IsLikedByCurrentUser = await _postService.IsPostLikedByUser(id, userId, cancellationToken);
        return Ok(postResponseDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostRequestDTO request, CancellationToken cancellationToken)
    {
        Post post = _mapper.Map<Post>(request);
        post.PostedAt = DateTime.Now;
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
    public async Task<IActionResult> Like(int postId, string userId, CancellationToken cancellationToken)
    {
        // first check if the user has already liked the post


        Post? post = await _postService.Like(postId, userId, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {postId} not found.");
        }
        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(post);
        postResponseDTO.IsLikedByCurrentUser = await _postService.IsPostLikedByUser(postId, userId, cancellationToken);
        return Ok(postResponseDTO);
    }

    [HttpPost("Unlike")]
    public async Task<IActionResult> Unlike(int postId, string userId, CancellationToken cancellationToken)
    {
        Post? post = await _postService.Unlike(postId, userId, cancellationToken);
        if (post == null)
        {
            return NotFound($"Post with ID {postId} not found.");
        }
        PostResponseDTO postResponseDTO = _mapper.Map<PostResponseDTO>(post);
        postResponseDTO.IsLikedByCurrentUser = await _postService.IsPostLikedByUser(postId, userId, cancellationToken);
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