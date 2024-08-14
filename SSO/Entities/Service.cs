using System.ComponentModel.DataAnnotations;
using NodaTime;
using SSO.Enums;

namespace SSO.Entities;

public class Service(string name)
{
    public long Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; } = name;
    public string? Description { get; set; }
    public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public Status Status { get; set; } = Status.Active;
    
    public List<AuthData> AuthDatas { get; set; } = new();
    public List<User> Users { get; set; } = new();
}