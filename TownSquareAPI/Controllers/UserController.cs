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

    public UserController(UserService userService)
    {
        _userService = userService;
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
        var token = TokenService.GenerateToken(user.Email);
        return Ok(new { token });
    }

    [HttpGet("GetById/{userId}")]
    public IActionResult GetById(int userId)
    {
        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }
        return Ok(user);
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