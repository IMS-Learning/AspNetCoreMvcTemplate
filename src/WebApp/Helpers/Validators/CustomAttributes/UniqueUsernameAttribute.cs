using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebApp.Helpers.Constants;

namespace WebApp.Helpers.Validators.CustomAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class ValidUsernameAttribute : ValidationAttribute
{
    public ValidUsernameAttribute()
        : base("'{0}' is not a valid username. Use 3-50 alphanumeric characters or underscores.")
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        var username = value.ToString()!;
        var isValid = Regex.IsMatch(username, ValidationPatterns.USERNAME);

        return isValid
            ? ValidationResult.Success
            : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}
