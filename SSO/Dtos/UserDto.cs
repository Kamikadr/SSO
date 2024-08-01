using System.ComponentModel.DataAnnotations;

namespace SSO.Dtos;

public record UserDto(string name, string password)
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = name;
    
    [MaxLength(100)]
    public string Password { get; set; } = password;
}