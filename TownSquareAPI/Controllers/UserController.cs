using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.User;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;

    public UserController(UserService userService, TokenService tokenService, IMapper mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] User user)
    {
        // email must be unique
        var existingUser = _userService.GetUserByEmail(user.Email);
        if (existingUser != null)
        {
            return BadRequest("Email already exists.");
        }

        // password must be hashed
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetById), new { userId = user.Id }, user);
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // check if user with email exists
        var user = _userService.GetUserByEmail(request.Email);
        if (user == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        // check if password is correct
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!isPasswordValid)
        {
            return Unauthorized("Invalid credentials.");
        }

        // generate a token
        var token = _tokenService.GenerateToken(user.Email);
        return Ok(new { token });
    }

    [HttpPost("ValidateToken")]
    public async Task<IActionResult> ValidateToken([FromBody] TokenRequest request)
    {
        // validate the token
        string? email = await _tokenService.ValidateTokenAsync(request.Token);
        if (email == null)
        {
            return Unauthorized("Invalid token.");
        }
        // get the user by email
        var user = _userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // map the user to a UserResponseDTO
        UserResponseDTO userResponseDTO = _mapper.Map<UserResponseDTO>(user);
        return Ok(userResponseDTO);
    }

    [HttpGet("GetById/{userId}")]
    public IActionResult GetById(int userId)
    {
        User? user = _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }
        return Ok(user);
    }

    [HttpGet("GetAllByCommunityId")]
    public IActionResult GetAllByCommunity(int communityId)
    {
        List<User> users = _userService.GetAllUsersByCommunityId(communityId);

        if (users == null || users.Count == 0)
        {
            return NotFound($"No users found in community with ID {communityId}.");
        }

        // use AutoMapper to map the list of users to a list of UserResponseDTO
        List<UserResponseDTO> userResponseDTOs = _mapper.Map<List<UserResponseDTO>>(users);
        return Ok(userResponseDTOs);
    }

    [HttpPut("Update/{userId}")]
    public IActionResult Update(int userId, [FromBody] UserRequestDTO updateUserRequest)
    {
        _userService.UpdateUser(userId, updateUserRequest.Name, updateUserRequest.Description);
        return Ok("Description updated.");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        _userService.DeleteUserById(userId);
        return Ok("User deleted.");
    }
}