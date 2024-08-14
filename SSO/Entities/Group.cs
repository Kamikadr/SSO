using System.ComponentModel.DataAnnotations;

namespace SSO.Entities;

public class Group(string name)
{
    public long Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = name;
    [MaxLength(500)]
    public string? Description { get; set; }

    public List<GroupRole> GroupRoles { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
    
    public List<UserGroup> UserGroups { get; set; } = new();
    public List<User> Users { get; set; } = new();
}