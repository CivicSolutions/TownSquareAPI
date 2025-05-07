namespace TownSquareAPI.Models;

public class UserCommunity
{
    public int UserId { get; set; }
    public int CommunityId { get; set; }
    public RequestStatus Status { get; set; }
}

public enum RequestStatus
{
    Pending,
    Accepted,
    Declined,
    Expired
}