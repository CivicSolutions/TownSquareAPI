using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace TownSquareAPI.Services;

public class TokenService
{
    // get JWT_SECRET from .env
    private static readonly string Secret = Environment.GetEnvironmentVariable("JWT_SECRET") ??
        throw new InvalidOperationException("JWT secret is not configured.");

    public string GenerateToken(string email)
    {
        byte[] key = Encoding.UTF8.GetBytes(Secret);
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, email)}),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature)
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(descriptor);
    }

    public async Task<ClaimsPrincipal?> GetPrincipalAsync(string token)
    {
        try
        {
            byte[] key = Convert.FromBase64String(Secret);
            var parameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var handler = new JsonWebTokenHandler();
            var result = await handler.ValidateTokenAsync(token, parameters);

            if (!result.IsValid)
                return null;

            var claimsIdentity = result.ClaimsIdentity;
            return new ClaimsPrincipal(claimsIdentity);
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> ValidateTokenAsync(string token)
    {


        // return userId if valid
        var principal = await GetPrincipalAsync(token);
        if (principal == null)
            return null;
        var emailClaim = principal.FindFirst(ClaimTypes.Name);
        if (emailClaim == null)
            return null;
        var email = emailClaim.Value;
        // check if user with email exists
        // var user = _userService.GetUserByEmail(email);
        // if (user == null)
        //     return null;
        // return user.Id.ToString();
        return email;

    }
}
