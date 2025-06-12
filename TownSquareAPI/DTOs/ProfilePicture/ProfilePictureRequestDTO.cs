namespace TownSquareAPI.DTOs.ProfilePicture;

public class ProfilePictureRequestDTO
{
    public byte[] Picture { get; set; }
    public int UserId { get; set; }
    public bool IsDeafult { get; set; }
}
