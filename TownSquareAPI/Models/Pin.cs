namespace TownSquareAPI.Models;

public class Pin
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PostedAt { get; set; }
    public double XCord { get; set; }
    public double YCord { get; set; }
    public int CommunityId { get; set; }
    public int Pintype { get; set; }
}