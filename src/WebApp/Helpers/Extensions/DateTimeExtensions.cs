namespace WebApp.Helpers.Extensions;

public static class DateTimeExtensions
{
    public static string ToRelativeTime(this DateTime dateTime)
    {
        var diff = DateTime.UtcNow - dateTime;

        return diff.TotalSeconds switch
        {
            < 60 => "just now",
            < 3600 => $"{(int)diff.TotalMinutes} minute(s) ago",
            < 86400 => $"{(int)diff.TotalHours} hour(s) ago",
            < 2592000 => $"{(int)diff.TotalDays} day(s) ago",
            _ => dateTime.ToString(Constants.AppConfig.DEFAULT_DATE_FORMAT)
        };
    }

    public static bool IsWeekend(this DateTime dateTime)
    {
        return dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    public static DateTime StartOfDay(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    public static DateTime EndOfDay(this DateTime dateTime)
    {
        return dateTime.Date.AddDays(1).AddTicks(-1);
    }
}
