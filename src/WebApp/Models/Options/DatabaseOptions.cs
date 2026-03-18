namespace WebApp.Models.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public int CommandTimeout { get; set; } = 30;
    public int MaxPoolSize { get; set; } = 10;
}
