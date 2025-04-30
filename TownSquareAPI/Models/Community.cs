namespace TownSquareAPI.Models
{
    public class Community
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsLicensed { get; set; }
        public string Description { get; set; }
    }
}