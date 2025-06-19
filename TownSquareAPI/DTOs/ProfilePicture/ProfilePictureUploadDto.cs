namespace TownSquareAPI.DTOs.ProfilePicture;

public class ProfilePictureUploadDto
{
    public string UserId { get; set; }
    public IFormFile File { get; set; } = default!;
}
