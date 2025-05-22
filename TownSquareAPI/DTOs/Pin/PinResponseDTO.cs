namespace TownSquareAPI.DTOs.Pin;

public class PinResponseDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PostedAt { get; set; }
    public double XCord { get; set; }
    public double YCord { get; set; }
    public int CommunityId { get; set; }
    public int Pintype { get; set; }
}
