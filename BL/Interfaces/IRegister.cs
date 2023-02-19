namespace BL;

public interface IRegister
{
    Task<bool> UserRegesterAsync(AddUserDTO model);
    Task<bool> LoginAsync(LoginDTO model);
    Task<bool> Verification(int verifyCode);
    Task<bool> LogoutAsync(Guid id);
    Task<ReadUserDTO> GetUserByEmailAsync(string email);
}
