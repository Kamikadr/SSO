using NodaTime;

namespace SSO.Entities;

public class AuthData(long userId, long serviceId)
{
    public long AuthDataId { get; init; }
    
    public string? RefreshToken { get; set; }
    public Instant CreatedAt { get; init; } = SystemClock.Instance.GetCurrentInstant();
    public DateTime? UpdatedAt { get; set; }

    public long UserId { get; init; } = userId;
    public required User User { get; init; }

    public long ServiceId { get; init; } = serviceId;
    public required Service Service { get; init; }
}