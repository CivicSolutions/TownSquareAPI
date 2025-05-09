﻿using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TownSquareAPI.Services;

public class TokenService
{
    // get JWT_SECRET from .env
    private static readonly string Secret = Environment.GetEnvironmentVariable("JWT_SECRET") ??
        throw new InvalidOperationException("JWT secret is not configured.");

    public static string GenerateToken(string username)
    {
        byte[] key = Convert.FromBase64String(Secret);
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, username)}),
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
