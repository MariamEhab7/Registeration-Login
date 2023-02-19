using System.ComponentModel.DataAnnotations;

namespace BL;

public class LoginDTO
{
    [Required]
    public string? Email { get; set; }
    [Required]
    [PasswordValidationAttribute]
    public string? Password { get; set; }
}
