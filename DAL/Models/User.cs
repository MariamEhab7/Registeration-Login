using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL;

public class User
{
    public Guid Id { get; set; }
    [Required]
    public string? FirstName { get; set; } 
    [Required]
    public string? LastName { get; set; } 
    [Required]
    [JsonIgnore]
    public string? Password { get; set; } 
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Phone { get; set; }

    public string? Nationality { get; set; }

    public byte[]? Photo { get; set; }

    public string? Role { get; set; }
    public string? Token { get; set; }
    public bool IsActive { get; set; }

    public Country country { get; set; }
}
