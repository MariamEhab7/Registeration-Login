using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BL;

public class UserServices : IUser
{
    #region Dependancy Injection
    private readonly UserContext _context;
    public UserServices(UserContext context)
    {
        _context = context;
    }
    #endregion

    public async Task<User> GetUserAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
    public async Task<bool> DeleteUserAsync(User user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
