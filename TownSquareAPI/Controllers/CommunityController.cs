﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.Community;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommunityController : ControllerBase
{
    private readonly CommunityService _communityService;
    private readonly IMapper _mapper;

    public CommunityController(CommunityService communityService, IMapper mapper)
    {
        _communityService = communityService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        List<Community> communities = await _communityService.GetAll(cancellationToken);
        List<CommunityResponseDTO> communityDTOs = _mapper.Map<List<CommunityResponseDTO>>(communities); // kind of useless when the mapping is 1:1 but it's good practice (edit: removed isLicensed so it now makes sense)
        return Ok(communityDTOs);
    }

    [HttpGet("GetByName")]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        Community? community = await _communityService.GetByName(name, cancellationToken);

        if (community == null)
        {
            return NotFound($"Community with name {name} not found.");
        }

        CommunityResponseDTO communityDTO = _mapper.Map<CommunityResponseDTO>(community);
        return Ok(communityDTO);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        Community? community = await _communityService.GetById(id, cancellationToken);

        if (community == null)
        {
            return NotFound($"Community with ID {id} not found.");
        }

        CommunityResponseDTO communityDTO = _mapper.Map<CommunityResponseDTO>(community);
        return Ok(communityDTO);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CommunityRequestDTO request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Location))
        {
            return BadRequest("Name and location are required.");
        }

        Community community = _mapper.Map<Community>(request);
        Community createdCommunity = await _communityService.Create(community, cancellationToken);
        CommunityResponseDTO communityDto = _mapper.Map<CommunityResponseDTO>(createdCommunity);
        return CreatedAtAction(nameof(GetById), new { id = communityDto.Id }, communityDto);
    }

    [HttpPut("Update/{communityId}")]
    public async Task<IActionResult> Update(int communityId, [FromBody] CommunityRequestDTO request, CancellationToken cancellationToken)
    {
        Community community = _mapper.Map<Community>(request);
        Community? updatedCommunity = await _communityService.Update(communityId, community, cancellationToken);

        if (updatedCommunity == null)
        {
            return NotFound($"Community with ID {communityId} not found.");
        }

        CommunityResponseDTO communityDto = _mapper.Map<CommunityResponseDTO>(updatedCommunity);
        return CreatedAtAction(nameof(GetById), new { id = communityDto.Id }, communityDto); // thoughts on using CreatedAtAction instead of Ok here?
    }

    [HttpDelete("Delete/{communityId}")]
    public async Task<IActionResult> DeleteCommunity(int communityId, CancellationToken cancellationToken)
    {
        bool isDeleted = await _communityService.Delete(communityId, cancellationToken);

        if (!isDeleted)
        {
            return NotFound($"Community with ID {communityId} not found.");
        }

        return NoContent();
    }

    [HttpPut("RequestMembership")]
    public async Task<IActionResult> RequestMembership(string userId, int communityId, CancellationToken cancellationToken)
    {
        await _communityService.CreateMembershipRequest(userId, communityId, cancellationToken);
        return Ok("Membership request submitted.");
    }
}