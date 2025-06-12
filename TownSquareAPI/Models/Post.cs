namespace TownSquareAPI.Models;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public DateTime PostedAt { get; set; }
    public int IsNews { get; set; }
    public int CommunityId { get; set; }
    public int LikeCount { get; set; }
}