using System.ComponentModel.DataAnnotations;

namespace SSO.Entities;

public class Role(string name)
{
    public long Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = name;
    
    [MaxLength(500)]
    public string? Description { get; set; }

    public List<GroupRole> GroupRoles { get; set; } = new();
    public List<Group> Groups { get; set; } = new();
    
    public List<Permission> Permissions { get; set; } = new();
    public List<RolePermission> RolePermissions { get; set; } = new();
}