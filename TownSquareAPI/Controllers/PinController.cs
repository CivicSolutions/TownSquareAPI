using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.Pin;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PinController : ControllerBase
{
    private readonly PinService _pinService;
    private readonly IMapper _mapper;

    public PinController(PinService pinService, IMapper mapper)
    {
        _pinService = pinService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var pins = await _pinService.GetAll(cancellationToken);
        return Ok(pins);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var pin = await _pinService.GetById(id, cancellationToken);
        if (pin == null)
        {
            return NotFound($"Pin with ID {id} not found.");
        }
        return Ok(pin);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PinRequestDTO request, CancellationToken cancellationToken)
    {
        Pin pin = _mapper.Map<Pin>(request);
        Pin createdPin = await _pinService.Create(pin, cancellationToken);
        PinResponseDTO pinDto = _mapper.Map<PinResponseDTO>(createdPin);
        return CreatedAtAction(nameof(GetById), new { id = pinDto.Id }, pinDto);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int pinId, [FromBody] PinRequestDTO request, CancellationToken cancellationToken)
    {
        Pin pin = _mapper.Map<Pin>(request);
        Pin? updatedPin = await _pinService.Update(pinId, pin, cancellationToken);

        if (updatedPin == null)
        {
            return NotFound($"Pin with ID {pinId} not found.");
        }

        PinResponseDTO pinDto = _mapper.Map<PinResponseDTO>(updatedPin);
        return CreatedAtAction(nameof(GetById), new { id = pinDto.Id }, pinDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int pinId, CancellationToken cancellationToken)
    {
        bool deleted = await _pinService.Delete(pinId, cancellationToken);

        if (!deleted)
        {
            return NotFound($"Pin with ID {pinId} not found.");
        }

        return NoContent();
    }
}