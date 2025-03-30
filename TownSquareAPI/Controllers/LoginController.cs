using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.Data;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public LoginController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserLoginRequest request)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Name == request.Username);

        if (user == null || user.Password != request.Password)
        {
            return Unauthorized("Invalid login credentials.");
        }

        return Ok(new { Message = "Login successful!", UserId = user.Id });
    }
}

public class UserLoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}