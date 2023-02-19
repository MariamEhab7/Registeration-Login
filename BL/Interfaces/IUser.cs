using DAL;

namespace BL;

public interface IUser
{
    Task<User> GetUserAsync(Guid id);
    Task<bool> DeleteUserAsync(User user);
}
