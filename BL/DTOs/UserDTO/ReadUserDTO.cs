namespace BL;

public class ReadUserDTO
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? Token { get; set; }
    public bool isActive { get; set; }
}
