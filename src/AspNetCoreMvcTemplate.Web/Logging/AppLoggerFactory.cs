namespace AspNetCoreMvcTemplate.Web.Logging;

public static class AppLoggerFactory
{
    public static ILogger<T> CreateLogger<T>(ILoggerFactory loggerFactory)
        => loggerFactory.CreateLogger<T>();

    public static ILogger CreateLogger(ILoggerFactory loggerFactory, string categoryName)
        => loggerFactory.CreateLogger(categoryName);
}
