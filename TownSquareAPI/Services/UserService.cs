using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TownSquareAPI.Models;

namespace TownSquareAPI.Services;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<List<ApplicationUser>> GetUserByFirstAndLastNameAsync(string? firstName, string? lastName)
    {
        var query = _userManager.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
        {
            var lowerFirstName = firstName.ToLower();
            query = query.Where(u => u.FirstName.ToLower().Contains(lowerFirstName));
        }
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            var lowerLastName = lastName.ToLower();
            query = query.Where(u => u.LastName.ToLower().Contains(lowerLastName));
        }

        return await query.ToListAsync();
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
    {
        return await _userManager.DeleteAsync(user);
    }
}