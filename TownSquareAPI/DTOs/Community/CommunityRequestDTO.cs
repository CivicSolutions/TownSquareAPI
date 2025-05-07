namespace TownSquareAPI.DTOs.Community;

public class CommunityRequestDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public bool IsLicensed { get; set; }
}