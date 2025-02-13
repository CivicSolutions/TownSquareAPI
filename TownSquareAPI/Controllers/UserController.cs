using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers
{
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
            return Ok("Benutzer erstellt.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserByUsername(request.Username);
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Ungültige Anmeldedaten.");
            }
            return Ok("Erfolgreich eingeloggt.");
        }

        [HttpPut("UpdateBio/{userId}")]
        public IActionResult UpdateBio(int userId, [FromBody] string newBio)
        {
            _userService.UpdateUserBio(userId, newBio);
            return Ok("Bio aktualisiert.");
        }
    }
}