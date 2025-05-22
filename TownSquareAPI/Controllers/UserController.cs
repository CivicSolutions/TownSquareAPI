using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.ApplicationUser;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IMapper _mapper;

    public UserController(UserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ApplicationUser> users = await _userService.GetAllUsersAsync();
        List<UserResponseDTO> userResponseDTOs = _mapper.Map<List<UserResponseDTO>>(users);
        return Ok(userResponseDTOs);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetById(string userId)
    {
        ApplicationUser? user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"No user found with ID {userId}.");
        }
        UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
        return Ok(userResponseDTO);
    }

    [HttpGet("GetByFirstAndLastName")]
    public async Task<IActionResult> GetByFirstAndLastName([FromQuery] string? firstName, [FromQuery] string? lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
        {
            return BadRequest("At least one of firstName or lastName must be provided.");
        }

        var users = await _userService.GetUserByFirstAndLastNameAsync(firstName, lastName);
        if (users == null || users.Count == 0)
        {
            return NotFound("No users found with the given criteria.");
        }
        var userResponseDTOs = _mapper.Map<List<UserResponseDTO>>(users);
        return Ok(userResponseDTOs);
    }



    [HttpGet("GetByEmail")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        ApplicationUser? user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound($"No user found with email {email}.");
        }
        UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
        return Ok(userResponseDTO);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(string userId, [FromBody] UserRequestDTO userRequestDTO)
    {
        ApplicationUser? user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"No user found with ID {userId}.");
        }
        _mapper.Map(userRequestDTO, user);
        var result = await _userService.UpdateUserAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        ApplicationUser? user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"No user found with ID {userId}.");
        }
        var result = await _userService.DeleteUserAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
}