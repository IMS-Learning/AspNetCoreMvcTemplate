using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebApp.Helpers.Constants;

namespace WebApp.Helpers.Validators.CustomAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class ValidEmailAttribute : ValidationAttribute
{
    public ValidEmailAttribute()
        : base("'{0}' is not a valid email address.")
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        var email = value.ToString()!;
        var isValid = Regex.IsMatch(email, ValidationPatterns.EMAIL, RegexOptions.IgnoreCase);

        return isValid
            ? ValidationResult.Success
            : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}
