using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.Models;
using TownSquareAPI.Services;

namespace TownSquareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HelpPostController : ControllerBase
{
    private readonly HelpPostService _helpPostService;

    public HelpPostController(HelpPostService helpPostService)
    {
        _helpPostService = helpPostService;
    }

    [HttpGet("GetHelpPosts")]
    public IActionResult GetHelpPosts()
    {
        var helpPosts = _helpPostService.GetHelpPosts();
        return Ok(helpPosts);
    }

    [HttpPost("AddHelpPost")]
    public IActionResult AddHelpPost([FromBody] HelpPost helpPost)
    {
        _helpPostService.AddHelpPost(helpPost);
        return Ok("Help post added.");
    }
}