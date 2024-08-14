using System.ComponentModel.DataAnnotations;
using NodaTime;
using SSO.Enums;

namespace SSO.Entities;

public record User(string Email, string PasswordHash, string FullName)
{
    public long Id { get; set; }
    
    [MaxLength(100)]
    public string? FullName { get; set; }
    
    [MaxLength(100)]
    public string Username { get; set; } = FullName;
    
    [MaxLength(100)]
    public string PasswordHash { get; set; } = PasswordHash;
    
    [MaxLength(100)]
    public string Email { get; set; } = Email;
    public Instant RegisteredAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public Status Status { get; set; } = Status.Active;

    public List<Service> Services { get; set; } = new();
    public List<AuthData> AuthDatas { get; set; } = new();
    
    public List<Group> Groups { get; set; } = new();
    public List<UserGroup> UserGroups { get; set; } = new();
}