namespace SSO.Entities;

public class UserGroup(long userId, long groupId, long invitedByUserId)
{
    public long UserId { get; set; } = userId;
    public required User User { get; set; }
    
    public long GroupId { get; set; } = groupId;
    public required Group Group { get; set; }
    
    public long InvitedByUserId { get; init; } = invitedByUserId;
    public required User InvitedByUser { get; init; }
}