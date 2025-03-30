namespace TownSquareAPI.DTOs;

public class CreatePostDto
{
    public string content { get; set; }
    public int user_id { get; set; }
    public int isnews { get; set; }
    public int? community_id { get; set; }
}