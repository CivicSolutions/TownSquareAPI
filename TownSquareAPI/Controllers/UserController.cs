using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs;
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
            _userService.CreateUser(user);
            return Ok("User created.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserByEmailAndPassword(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid login credentials.");
            }
            return Ok("Login successful.");
        }

        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }
            return Ok(user);
        }

        [HttpPut("UpdateDescription/{userId}")]
        public IActionResult UpdateDescription(int userId, [FromBody] UpdateDescriptionDto updateDescriptionDto)
        {
            _userService.UpdateUser(userId, updateDescriptionDto.NewUsername, updateDescriptionDto.NewDescription);
            return Ok("Description updated.");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUserById(userId);
            return Ok("User deleted.");
        }
    }