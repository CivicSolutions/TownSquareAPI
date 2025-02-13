namespace TownSquareAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsNews { get; set; }
        public int? CommunityId { get; set; }
    }
}