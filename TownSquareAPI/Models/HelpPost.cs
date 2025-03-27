namespace TownSquareAPI.Models
{
    public class HelpPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Telephone { get; set; }
        public DateTime PostedAt { get; set; }
        public int UserId { get; set; }
    }
}