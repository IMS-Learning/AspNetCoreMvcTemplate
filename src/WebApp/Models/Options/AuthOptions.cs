namespace WebApp.Models.Options;

public class AuthOptions
{
    public string JwtSecret { get; set; } = string.Empty;
    public int TokenExpiryMinutes { get; set; } = 60;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
