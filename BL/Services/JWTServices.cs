using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BL;

public class JWTServices : IJwtInitializer
{
    public string GenerateToken(IList<Claim> claims)
    {
        var SymmetricKey = UserKey.CreateKey();
        var signingCredentials = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256Signature);

        var jwt = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddYears(1),
            notBefore: DateTime.Now
            );

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenStirng = tokenHandler.WriteToken(jwt);

        return tokenStirng;
    }
}
