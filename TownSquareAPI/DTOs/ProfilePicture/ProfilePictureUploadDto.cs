using Microsoft.AspNetCore.Mvc;

namespace TownSquareAPI.DTOs.ProfilePicture;

public class ProfilePictureUploadDto
{
    [FromForm(Name = "UserId")]
    public string UserId { get; set; } = null!;

    [FromForm(Name = "Picture")]
    public IFormFile Picture { get; set; } = null!;
}
