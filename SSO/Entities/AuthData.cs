using NodaTime;

namespace SSO.Entities;

public class AuthData(long userId, long serviceId)
{
    public long AuthDataId { get; init; }
    
    public string? RefreshToken { get; set; }
    public Instant CreatedAt { get; init; } = SystemClock.Instance.GetCurrentInstant();
    public Instant? UpdatedAt { get; set; }

    public long UserId { get; init; } = userId;
    public User User { get; init; } = null!;

    public long ServiceId { get; init; } = serviceId;
    public Service Service { get; init; }  = null!;
}