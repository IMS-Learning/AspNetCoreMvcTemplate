namespace WebApp.Helpers.Constants;

public static class ValidationPatterns
{
    public const string EMAIL = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public const string USERNAME = @"^[a-zA-Z0-9_]{3,50}$";
    public const string PHONE = @"^\+?[1-9]\d{1,14}$";
    public const string STRONG_PASSWORD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
    public const string SAFE_TEXT = @"^[a-zA-Z0-9\s\-_.,!?'""]+$";
}
