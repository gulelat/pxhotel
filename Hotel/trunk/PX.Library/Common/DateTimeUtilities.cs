using System;

namespace PX.Library.Common
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
    }
}
