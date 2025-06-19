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
    public IActionResult GetProfilePictureUrl(string userId)
    {
        var imageUrl = Url.Action(nameof(GetProfilePictureFile), "ProfilePicture", new { userId }, Request.Scheme);

        return Ok(new { imageUrl });
    }

    [HttpGet("ImageFile")]
    [AllowAnonymous] // If you want it public; keep [Authorize] if not
    public async Task<IActionResult> GetProfilePictureFile(string userId, CancellationToken cancellationToken)
    {
        var picture = await _profilePictureService.GetByUserIdAsync(userId, cancellationToken);

        if (picture == null || string.IsNullOrWhiteSpace(picture.Picture))
        {
            return NotFound("Profile picture not found.");
        }

        string base64String = picture.Picture;

        // Remove prefix like "data:image/png;base64,"
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

        if (imageBytes.Length == 0)
        {
            return NotFound("Profile picture is empty.");
        }

        string? contentType = GetImageContentType(imageBytes);
        if (contentType == null)
        {
            return BadRequest("Unsupported image format.");
        }

        return File(imageBytes, contentType);
    }

    private static string? GetImageContentType(byte[] imageData)
    {
        if (imageData.Length >= 8 &&
            imageData[0] == 0x89 && imageData[1] == 0x50 &&
            imageData[2] == 0x4E && imageData[3] == 0x47)
        {
            return "image/png";
        }

        if (imageData.Length >= 3 &&
            imageData[0] == 0xFF && imageData[1] == 0xD8 && imageData[2] == 0xFF)
        {
            return "image/jpeg";
        }

        return null;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadProfilePicture([FromForm] ProfilePictureUploadDto dto, CancellationToken cancellationToken)
    {
        if (dto.Picture == null || dto.Picture.Length == 0)
            return BadRequest("No file uploaded.");

        using var memoryStream = new MemoryStream();
        await dto.Picture.CopyToAsync(memoryStream, cancellationToken);
        byte[] imageBytes = memoryStream.ToArray();

        string base64Image = Convert.ToBase64String(imageBytes);

        var profilePicture = new ProfilePicture
        {
            UserId = dto.UserId,
            Picture = base64Image
        };

        await _profilePictureService.CreateOrReplaceAsync(profilePicture, cancellationToken);

        return Ok("Profile picture uploaded successfully.");
    }


}
