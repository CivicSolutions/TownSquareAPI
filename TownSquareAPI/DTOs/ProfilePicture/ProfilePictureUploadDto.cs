using Microsoft.AspNetCore.Mvc;

namespace TownSquareAPI.DTOs.ProfilePicture;

public class ProfilePictureUploadDto
{
    [FromForm(Name = "Picture")]
    public IFormFile Picture { get; set; } = default!;

    [FromForm]
    public string UserId { get; set; } = default!;
}
