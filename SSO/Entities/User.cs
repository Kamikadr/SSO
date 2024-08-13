using System.ComponentModel.DataAnnotations;

namespace SSO.Entities;

public record User(string Email, string Password, string Name)
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = Name;
    
    [MaxLength(100)]
    public string Password { get; set; } = Password;
    
    [MaxLength(100)]
    public string Email { get; set; } = Email;
}