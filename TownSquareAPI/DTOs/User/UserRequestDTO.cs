namespace TownSquareAPI.DTOs.User;

public class UserRequestDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
