using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownSquareAPI.DTOs.ProfilePicture;
using TownSquareAPI.Models;
using TownSquareAPI.Services;


namespace TownSquareAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfilePictureController : ControllerBase
{
    private readonly ProfilePictureService _profilePictureService;

    public ProfilePictureController(ProfilePictureService profilePictureService)
    {
        _profilePictureService = profilePictureService;
    }

    [HttpGet("Image")]
    public async Task<IActionResult> GetProfilePictureByUserId(int userId, CancellationToken cancellationToken)
    {
        var picture = await _profilePictureService.GetByUserIdAsync(userId, cancellationToken);

        if (picture == null || picture.Picture == null || picture.Picture.Length == 0)
        {
            return NotFound("Profile picture not found.");
        }

        // You may determine the content type from metadata, or hardcode based on how you store it
        string contentType = "image/jpeg"; // or "image/png", depending on your image format

        return File(picture.Picture, contentType);
    }

}
