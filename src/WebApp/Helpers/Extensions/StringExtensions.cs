namespace WebApp.Helpers.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;
        return string.Concat(value.AsSpan(0, maxLength - suffix.Length), suffix);
    }

    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
    }

    public static bool IsValidEmail(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;
        return System.Text.RegularExpressions.Regex.IsMatch(
            value,
            Constants.ValidationPatterns.EMAIL,
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    public static string ToSlug(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;
        return value.ToLower()
            .Replace(" ", "-")
            .Replace("_", "-");
    }
}
