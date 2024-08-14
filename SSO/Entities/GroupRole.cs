namespace SSO.Entities;

public class GroupRole(long groupId, long roleId, long addedByUserId)
{
    public long GroupId { get; init; } = groupId;
    public required Group Group { get; init; }
    
    public long RoleId { get; init; } = roleId;
    public required Role Role { get; init; }
    
    public long AddedByUserId { get; init; } = addedByUserId;
    public required User AddedByUser { get; init; }
}