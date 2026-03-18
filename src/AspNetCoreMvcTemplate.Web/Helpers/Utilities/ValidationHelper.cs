using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AspNetCoreMvcTemplate.Web.Helpers.Constants;

namespace AspNetCoreMvcTemplate.Web.Helpers.Utilities;

public static class ValidationHelper
{
    public static bool IsValidEmail(string email)
        => !string.IsNullOrWhiteSpace(email) &&
           Regex.IsMatch(email, ValidationPatterns.EMAIL, RegexOptions.IgnoreCase);

    public static bool IsValidUsername(string username)
        => !string.IsNullOrWhiteSpace(username) &&
           Regex.IsMatch(username, ValidationPatterns.USERNAME);

    public static bool IsStrongPassword(string password)
        => !string.IsNullOrWhiteSpace(password) &&
           Regex.IsMatch(password, ValidationPatterns.STRONG_PASSWORD);

    public static IList<ValidationResult> ValidateObject(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }
}
