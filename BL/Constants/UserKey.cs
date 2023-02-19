using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BL;

public static class UserKey
{
    public static SymmetricSecurityKey CreateKey()
    {
        var keyString = "hbfhejfbrejygsdkjgbfdjgubrukfn";
        var keyInBytes = Encoding.ASCII.GetBytes(keyString);
        var Key = new SymmetricSecurityKey(keyInBytes);
        return Key;
    }
}
