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

    public static string GenerateToken(string email)
    {
        Console.WriteLine(Secret);
        byte[] key = Encoding.UTF8.GetBytes(Secret);
        Console.WriteLine(key.Length);
        Console.WriteLine(key);
        Console.WriteLine(Encoding.UTF8.GetString(key));
        Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(Secret)));
        Console.WriteLine(Convert.ToBase64String(key));
        Console.WriteLine(Convert.ToBase64String(Convert.FromBase64String(Secret)));
        Console.WriteLine(Secret.Length);
        Console.WriteLine(Secret);
        Console.WriteLine(key[key.Length - 1]);
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

    public static async Task<ClaimsPrincipal?> GetPrincipalAsync(string token)
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

    public static async Task<string?> ValidateTokenAsync(string token)
    {
        string? username = null;
        ClaimsPrincipal? principal = await GetPrincipalAsync(token);

        if (principal == null)
            return null;

        try
        {
            var identity = principal.Identity as ClaimsIdentity;
            var usernameClaim = identity?.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;
        }
        catch (NullReferenceException)
        {
            return null;
        }

        return username;
    }
}
