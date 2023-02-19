using DAL;
using System.Security.Claims;

namespace BL;

public interface IClaimsInitializer
{
    List<Claim> CreateClaims(User user);
}
