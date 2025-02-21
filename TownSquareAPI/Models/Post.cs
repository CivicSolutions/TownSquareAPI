namespace TownSquareAPI.Models
{
    public class Post
    {
        public int id { get; set; }
        public string content { get; set; }
        public int user_id { get; set; } 
        public DateTime posttime { get; set; } 
        public int isnews { get; set; } 
        public int? community_id { get; set; } 
    }
}