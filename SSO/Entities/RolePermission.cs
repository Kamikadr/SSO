using NodaTime;

namespace SSO.Entities;

public class RolePermission(long permissionId, long roleId, long addedByUserId)
{
    public long PermissionId { get; init; } = permissionId;
    public required Permission Permission { get; init; }

    public long RoleId { get; init; } = roleId;
    public required Role Role { get; init; }

    public Instant AddedAt = SystemClock.Instance.GetCurrentInstant();

    public long AddedByUserId { get; init; } = addedByUserId;
    public required User AddedByUser { get; init; }
}