using System.ComponentModel.DataAnnotations;

namespace SSO.Entities;

public class Permission(string name)
{
    public long Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = name;
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public List<Role> Roles { get; set; } = new();
    public List<RolePermission> RolePermissions { get; set; } = new();
}