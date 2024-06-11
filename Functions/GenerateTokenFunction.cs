using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LogApi.Models.DTOs.Login;
using Microsoft.IdentityModel.Tokens;

namespace LogApi.Functions;

public static class GenerateTokenFunction
{
    public static string Authenticate(this LoginUserRequest request, IConfiguration configuration)
    {
        var key = configuration.GetValue<string>("JwtConfig:Key");
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.NameIdentifier, request.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = "BRINS",
            // Audience = "Dummy"
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}