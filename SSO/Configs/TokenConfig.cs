namespace SSO.Configs;

public class TokenConfig
{
    public required string SecretKey { get; init; }
    public TimeSpan AccessTokenLifetimeInterval { get; init; }
    public TimeSpan RefreshTokenLifetimeInterval { get; init; }
}