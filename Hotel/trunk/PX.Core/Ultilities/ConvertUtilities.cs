using System;

namespace PX.Core.Ultilities
{
    public static class ConvertUtilities
    {
        public static T ToType<T>(this object value)
        {
            var type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Nullable type.

                if (value == null)
                {
                    // you may want to do something different here.
                    return default(T);
                }
                else
                {
                    // Get the type that was made nullable.
                    Type valueType = type.GetGenericArguments()[0];

                    // Convert to the value type.
                    object result = Convert.ChangeType(value, valueType);

                    // Cast the value type to the nullable type.
                    return (T)result;
                }
            }
            // Not nullable.
            return (T)Convert.ChangeType(value, typeof(T));
        } 

        public static bool ToBool(this object value, bool defaultValue)
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

        public static DateTime ToDate(this object value, DateTime defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return DateTime.ParseExact(value.ToString(), Configurations.Configurations.DateFormat, null);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ToInt(this object value, int defaultValue)
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

        public static int? ToNullableInt(this object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        public static long ToLong(this object value, long defaultValue)
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

        public static float ToFloat(this object value, float defaultValue)
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

        public static double ToDouble(this object value, double defaultValue)
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

        public static decimal ToDecimal(this object value, decimal defaultValue)
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

        public static string ToString(this object value, string defaultValue)
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

        public static bool ToBool(this object value)
        {
            return ToBool(value, false);
        }

        public static DateTime ToDate(this object value)
        {
            return ToDate(value, new DateTime(0));
        }

        public static int ToInt(this object value)
        {
            return ToInt(value, 0);
        }

        public static long ToLong(this object value)
        {
            return ToLong(value, 0);
        }

        public static float ToFloat(this object value)
        {
            return ToFloat(value, 0);
        }

        public static double ToDouble(this object value)
        {
            return ToDouble(value, 0);
        }

        public static decimal ToDecimal(this object value)
        {
            return ToDecimal(value, 0);
        }

        public static string ToString(this object value)
        {
            return ToString(value, "");
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

                shortDatePattern = string.IsNullOrEmpty(shortDatePattern) ? Configurations.Configurations.DateFormat : shortDatePattern;
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
