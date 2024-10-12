namespace ApiLogger.Serilog
{
    internal static class DateTimeExtensions
    {
        public static TimeSpan ToTimeSpan(this DateTime date)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);

            return dateTime.TimeOfDay;
        }
        public static string FormatHour(this TimeSpan timeSpan)
        {
            return timeSpan.ToString("hh':'mm':'ss'.'fff");
        }
    }
}
