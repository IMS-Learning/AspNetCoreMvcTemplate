namespace AspNetCoreMvcTemplate.Web.Helpers.Utilities;

public static class DateHelper
{
    public static string FormatDate(DateTime date, string format = "yyyy-MM-dd")
        => date.ToString(format);

    public static string FormatDateTime(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        => dateTime.ToString(format);

    public static int GetAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age))
            age--;
        return age;
    }

    public static bool IsExpired(DateTime expiryDate)
        => DateTime.UtcNow > expiryDate;
}
