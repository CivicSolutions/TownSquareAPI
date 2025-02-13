namespace TownSquareAPI.DTOs
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public bool IsNews { get; set; }
        public int? CommunityId { get; set; }
    }
}