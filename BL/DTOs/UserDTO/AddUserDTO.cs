using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BL;

public class AddUserDTO
{
    [Required]
    [StringLength(8, MinimumLength = 3)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 3)]
    public string? LastName { get; set; }

    [Required]
    [EmailValidation]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Required, Should start with letter")]
    [PasswordValidation]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    public string? Nationality { get; set; }

    public IFormFile Photo { get; set; }

    public CountryDTO? country { get; set; }

}
