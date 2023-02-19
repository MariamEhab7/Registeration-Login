using System.Security.Claims;

namespace BL;

public interface IJwtInitializer
{
    string GenerateToken(IList<Claim> claims);
}
