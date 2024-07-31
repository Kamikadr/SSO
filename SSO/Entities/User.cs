using System.ComponentModel.DataAnnotations;

namespace SSO.Entities;

public record User
{
    public long Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
}