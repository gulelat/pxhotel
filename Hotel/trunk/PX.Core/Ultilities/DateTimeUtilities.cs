using System;

namespace PX.Core.Ultilities
{
    public static class DateTimeUtilities
    {
        public static DateTime GetBeginDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 1);
        }

        public static DateTime GetEndDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static string ToShortDateString(this DateTime? date)
        {
            if (date.HasValue)
                return date.Value.ToString(Configurations.Configurations.DateFormat);
            return new DateTime(0).ToString(Configurations.Configurations.DateFormat);
        }
    }
}
