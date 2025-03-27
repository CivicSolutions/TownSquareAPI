namespace TownSquareAPI.DTOs
{
    public class CreateCommunityRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public bool isLicensed { get; set; }
    }
}