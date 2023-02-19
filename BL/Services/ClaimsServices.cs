using DAL;
using System.Security.Claims;

namespace BL;

public class ClaimsServices : IClaimsInitializer
{
    public List<Claim> CreateClaims(User user)
    {
        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

        return claims;
    }
}
