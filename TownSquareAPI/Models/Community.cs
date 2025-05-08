namespace TownSquareAPI.Models
{
    public class Community
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public bool isLicensed { get; set; }
        public string description { get; set; }
    }
}