namespace WebApp.Helpers.Extensions;

public static class ExceptionExtensions
{
    public static string GetFullMessage(this Exception exception)
    {
        if (exception.InnerException == null)
            return exception.Message;
        return $"{exception.Message} --> {exception.InnerException.GetFullMessage()}";
    }

    public static bool IsTransient(this Exception exception)
    {
        return exception is TimeoutException
            or HttpRequestException
            or TaskCanceledException;
    }
}
