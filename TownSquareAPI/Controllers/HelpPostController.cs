using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.HelpPost;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HelpPostController : ControllerBase
{
    private readonly HelpPostService _helpPostService;
    private readonly IMapper _mapper;

    public HelpPostController(HelpPostService HelpPostService, IMapper mapper)
    {
        _helpPostService = HelpPostService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        List<HelpPost> helpPosts = await _helpPostService.GetAll(cancellationToken);

        List<HelpPostResponseDTO> helpPostResponseDTOs = _mapper.Map<List<HelpPostResponseDTO>>(helpPosts);
        return Ok(helpPostResponseDTOs);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        HelpPost? helpPost = await _helpPostService.GetById(id, cancellationToken);

        if (helpPost == null)
        {
            return NotFound($"HelpPost with ID {id} not found.");
        }

        HelpPostResponseDTO helpPostResponseDTOs = _mapper.Map<HelpPostResponseDTO>(helpPost);
        return Ok(helpPostResponseDTOs);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HelpPostRequestDTO request, CancellationToken cancellationToken)
    {
        HelpPost helpPost = _mapper.Map<HelpPost>(request);
        helpPost.PostedAt = DateTime.Now;
        HelpPost createdHelpPost = await _helpPostService.Create(helpPost, cancellationToken);

        HelpPostResponseDTO helpPostResponseDTOs = _mapper.Map<HelpPostResponseDTO>(createdHelpPost);
        return CreatedAtAction(nameof(GetById), new { id = helpPostResponseDTOs.Id }, helpPostResponseDTOs);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] HelpPostRequestDTO request, CancellationToken cancellationToken)
    {
        HelpPost helpPost = _mapper.Map<HelpPost>(request);
        HelpPost? updatedHelpPost = await _helpPostService.Update(id, helpPost, cancellationToken);

        if (updatedHelpPost == null)
        {
            return NotFound($"HelpPost with ID {id} not found.");
        }

        HelpPostResponseDTO helpPostResponseDTOs = _mapper.Map<HelpPostResponseDTO>(updatedHelpPost);
        return CreatedAtAction(nameof(GetById), new { id = helpPostResponseDTOs.Id }, helpPostResponseDTOs);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        bool deleted = await _helpPostService.Delete(id, cancellationToken);

        if (!deleted)
        {
            return NotFound($"HelpPost with ID {id} not found.");
        }

        return NoContent();
    }
}