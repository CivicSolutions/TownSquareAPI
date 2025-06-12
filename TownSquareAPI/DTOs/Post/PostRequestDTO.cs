namespace TownSquareAPI.DTOs.Post;

public class PostRequestDTO
{
    public string Content { get; set; }
    public int IsNews { get; set; }
    public int CommunityId { get; set; }
    public string UserId { get; set; }
}