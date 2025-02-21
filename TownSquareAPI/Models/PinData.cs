namespace TownSquareAPI.Models
{
    public class PinData
    {
        public int id { get; set; }
        public int user_id { get; set; } 
        public string title { get; set; }
        public string description { get; set; }
        public DateTime posttime { get; set; }
        public double x_cord { get; set; } 
        public double y_cord { get; set; } 
        public int community_id { get; set; } 
        public int pintype { get; set; }
    }
}