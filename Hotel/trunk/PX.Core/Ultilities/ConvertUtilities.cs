using System;

namespace PX.Core.Ultilities
{
    public static class ConvertUtilities
    {
        public static bool ToBool(object value, bool defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ToDate(object value, DateTime defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return DateTime.ParseExact(value.ToString(), "ddMMyy", null);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ToInt(object value, int defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long ToLong(object value, long defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static float ToFloat(object value, float defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToSingle(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ToDouble(object value, double defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static decimal ToDecimal(object value, decimal defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ToString(object value, string defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBool(object value)
        {
            return ToBool(value, false);
        }

        public static DateTime ToDate(object value)
        {
            return ToDate(value, new DateTime(0));
        }

        public static int ToInt(object value)
        {
            return ToInt(value, 0);
        }

        public static long ToLong(object value)
        {
            return ToLong(value, 0);
        }

        public static float ToFloat(object value)
        {
            return ToFloat(value, 0);
        }

        public static double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }

        public static decimal ToDecimal(object value)
        {
            return ToDecimal(value, 0);
        }

        public static string ToString(object value)
        {
            return ToString(value, "");
        }

        // Deprecated
        public static int SafeInt(object o, int defaultValue)
        {
            return ToInt(o, defaultValue);
        }

        public static string SafeText(string value)
        {
            if (value == null)
                return "";

            return value;
        }

        public static DateTime StringToShortDate(string value, string shortDatePattern, DateTime defaultValue)
        {
            try
            {
                var dateInfo = new System.Globalization.DateTimeFormatInfo();

                shortDatePattern = string.IsNullOrEmpty(shortDatePattern) ? "dd/MM/yyyy" : shortDatePattern;
                dateInfo.ShortDatePattern = shortDatePattern;

                return Convert.ToDateTime(value, dateInfo);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime StringToShortDate(string value, string shortDatePattern)
        {
            return StringToShortDate(value, shortDatePattern, new DateTime(0));
        }
    }
}
