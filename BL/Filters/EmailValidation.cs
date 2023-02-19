using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BL;

sealed class EmailValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var email = (string?)value;
        var regex = new Regex(@"^[a-z0-9]@gmail\.com$");
        return regex.IsMatch(email);
    }
}
