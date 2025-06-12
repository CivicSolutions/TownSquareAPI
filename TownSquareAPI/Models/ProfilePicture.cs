namespace TownSquareAPI.Models
{
    public class ProfilePicture
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public int UserId { get; set; }
        public bool IsDeafult { get; set; }
    }
}
