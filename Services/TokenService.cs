using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogWeb.Configuration;
using BlogWeb.Extensions;
using BlogWeb.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogWeb.Services;

public class TokenService
{
    private readonly ApiConfiguration _apiConfiguration;

    public TokenService(IOptions<ApiConfiguration> options) => _apiConfiguration = options.Value;

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_apiConfiguration.JwtKey);
        var claims = user.GetClaims();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
