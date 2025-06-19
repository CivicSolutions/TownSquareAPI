using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TownSquareAPI.DTOs.ProfilePicture;

public class ProfilePictureResponseDTO
{
    public string Id { get; set; }
    public string Picture { get; set; }
    public int UserId { get; set; }
}
