namespace Dev.Assistant.Business.Core.Extensions;

/// <summary>
/// Provides extension methods for working with DateTime and TimeSpan.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Deconstructs a DateTime into its components.
    /// </summary>
    /// <param name="date">The DateTime to deconstruct.</param>
    /// <param name="year">The year component of the DateTime.</param>
    /// <param name="month">The month component of the DateTime.</param>
    /// <param name="minute">The minute component of the DateTime.</param>
    /// <remarks>
    /// This method extracts the year, month, and minute components from a DateTime.
    /// </remarks>
    public static void Deconstruct(this DateTime date, out int year, out int month, out int minute)
    {
        year = date.Year;
        month = date.Month;
        minute = date.Minute;
    }

    /// <summary>
    /// Deconstructs a TimeSpan into its components.
    /// </summary>
    /// <param name="time">The TimeSpan to deconstruct.</param>
    /// <param name="hours">The hour component of the TimeSpan.</param>
    /// <param name="remainingDays">The remaining days component of the TimeSpan.</param>
    /// <param name="months">The month component of the TimeSpan.</param>
    /// <param name="years">The year component of the TimeSpan.</param>
    /// <remarks>
    /// This method extracts the hours, remaining days, months, and years components from a TimeSpan.
    /// </remarks>
    public static void Deconstruct(this TimeSpan time, out int hours, out int remainingDays, out int months, out int years)
    {
        hours = time.Hours;
        months = (int)Math.Truncate(time.TotalDays % 365 / 30);
        years = (int)Math.Truncate(time.TotalDays / 365);
        remainingDays = (int)Math.Truncate(time.TotalDays % 365 % 30);
    }
}