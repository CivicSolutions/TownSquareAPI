namespace TownSquareAPI.DTOs.HelpPost;

public class HelpPostResponseDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public DateTime PostedAt { get; set; }
    public int CommunityId { get; set; }
    public int Price { get; set; }
    public string Telephone { get; set; }
}
