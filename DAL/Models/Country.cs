using System.ComponentModel.DataAnnotations;

namespace DAL;

public class Country
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Dial_code { get; set; } = "";
    
    [Required]
    [Key]
    public string Code { get; set; } = "";
}
