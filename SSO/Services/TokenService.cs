using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSO.Entities;

namespace SSO.Services;

public record TokenConfig(string SecretKey, TimeSpan AccessTokenDuration, TimeSpan RefreshTokenDuration);
public class TokenService(IOptions<TokenConfig> options)
{
    private readonly TokenConfig _config = options.Value ?? throw new ArgumentException( "", nameof(options));

    public (string AccessToken, string RefreshToken) GenerateTokens(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user);
        return (accessToken, refreshToken);
    }
    
    public string GenerateAccessToken(User user)
    {
        return GenerateToken(user, _config.AccessTokenDuration);
    }
    
    public string GenerateRefreshToken(User user)
    {
        return GenerateToken(user, _config.RefreshTokenDuration);
    }
    
    private string GenerateToken(User user, TimeSpan timeInterval)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: user.Email,
            audience: "client",
            expires: DateTime.Now.Add(timeInterval),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}