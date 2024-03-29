using System.Security.Claims;
using capstone.Interfaces;
using capstone.Models;
using capstone.Interfaces;
namespace capstone.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class TokenService : ITokenService
{
    private readonly string _secretKey;

    public TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["AppSettings:Secret"];
        if (string.IsNullOrEmpty(_secretKey))
        {
            throw new ArgumentException("SecretKey is missing in appsettings.json");
        }
    }

    public int? GetUserIdFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

            var userIdClaim = claimsPrincipal.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return null;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return null;
        }
    }
    
    
    
    public string GenerateToken(Iunifier user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        // Ensure that tokenString is not null before attempting to convert it to bytes
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.GetType().Name)
            }),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        if (tokenString == null)
        {
            throw new InvalidOperationException("Generated token string is null.");
        }

        return tokenString;
    }
    
    public string GetRoleFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

            var roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role);
            if (roleClaim != null)
            {
                return roleClaim.Value;
            }

            return null;
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            return null;
        }
    }

}
