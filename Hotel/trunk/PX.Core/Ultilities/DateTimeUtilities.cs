using System;
using PX.Core.Configurations.Constants;

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
            var a = date.Value.ToString(DefaultConstants.DateFormat);
            if (date.HasValue)
                return date.Value.ToString(DefaultConstants.DateFormat);
            return new DateTime(0).ToString(DefaultConstants.DateFormat);
        }
    }
}
