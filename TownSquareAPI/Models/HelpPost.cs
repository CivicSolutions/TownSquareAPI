namespace TownSquareAPI.Models
{
    public class HelpPost
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string telephone { get; set; }
        public DateTime helpposttime { get; set; }
        public int user_id { get; set; } 
    }
}