using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSO.Configs;
using SSO.Entities;

namespace SSO.Services;

public class TokenService(IOptions<TokenConfig> options)
{
    private readonly TokenConfig _config = options.Value ?? throw new ArgumentException( "Config is not found or validation error", nameof(options));

    public (string AccessToken, string RefreshToken) GenerateTokens(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user);
        return (accessToken, refreshToken);
    }
    
    public string GenerateAccessToken(User user)
    {
        return GenerateToken(user, _config.AccessTokenLifetimeInterval);
    }
    
    public string GenerateRefreshToken(User user)
    {
        return GenerateToken(user, _config.RefreshTokenLifetimeInterval);
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