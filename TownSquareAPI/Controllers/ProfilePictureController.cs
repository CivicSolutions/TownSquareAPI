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

        if (picture == null || string.IsNullOrWhiteSpace(picture.Picture))
        {
            return NotFound("Profile picture not found.");
        }

        string base64String = picture.Picture;

        // Optional: Remove data URL prefix if exists (like "data:image/png;base64,")
        if (base64String.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
        {
            var commaIndex = base64String.IndexOf(',');
            if (commaIndex >= 0)
            {
                base64String = base64String[(commaIndex + 1)..];
            }
        }

        byte[] imageBytes;
        try
        {
            imageBytes = Convert.FromBase64String(base64String);
        }
        catch (FormatException)
        {
            return BadRequest("Picture data is not in valid Base64 format.");
        }

        // Optional: If you want to detect content type dynamically from the byte array, you can add logic here.
        string contentType = "image/png"; // or set based on your actual image type

        if (imageBytes.Length == 0)
        {
            return NotFound("Profile picture is empty.");
        }

        return File(imageBytes, contentType);
    }

}
