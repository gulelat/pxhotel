using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace PX.Core.Ultilities
{
    public static class EnumUtilities
    {
        /// <summary>
        /// Gets all items for an enum type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        
        /// <summary>
        /// Gets all items for an enum type as Text = Name, Value = Id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetAllItemsFromEnum<T>() where T : struct
        {
            return GetAllItems<T>().Select(e => new SelectListItem
                {
                Text = e.ToString(),
                Value = Convert.ToInt32(e).ToString(CultureInfo.InvariantCulture)
            });
        }

        /// <summary>
        /// Gets all items for an enum type as Text = Name, Value = Id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetAllItemsFromEnumExcept<T>(int value) where T : struct
        {
            return GetAllItems<T>().Where(c => Convert.ToInt32(c) != value).Select(e => new SelectListItem
                {
                Text = e.ToString(),
                Value = Convert.ToInt32(e).ToString(CultureInfo.InvariantCulture)
            });
        }

        /// <summary>
        /// Gets all items for an enum type as Text = Name, Value = Name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetAllItemsWithStringValue<T>() where T : struct
        {
            return GetAllItems<T>().Select(e => new SelectListItem
                {
                Text = e.ToString(),
                Value = e.ToString()
            });
        }

        public static IEnumerable<SelectListItem> GetItemsFromEnum<T>(T selectedValue = default(T)) where T : struct
        {
            return from name in Enum.GetNames(typeof(T))
                   let enumValue = Convert.ToString(Enum.Parse(typeof(T), name, true))
                   select new SelectListItem
                   {
                       Text = GetEnumDescription(name, typeof(T)),
                       Value = enumValue,
                       Selected = name == selectedValue.ToString()
                   };
        }

        /// <summary>
        /// Determines whether the enum value contains a specific value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///     <c>true</c> if value contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// <code>
        /// EnumExample dummy = EnumExample.Combi;
        /// if (dummy.Contains<EnumExample>(EnumExample.ValueA))
        /// {
        ///     Console.WriteLine("dummy contains EnumExample.ValueA");
        /// }
        /// </code>
        /// </example>
        public static bool Contains<T>(this Enum value, T request)
        {
            var valueAsInt = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            var requestAsInt = Convert.ToInt32(request, CultureInfo.InvariantCulture);

            return requestAsInt.Equals(valueAsInt & requestAsInt);
        }

        public static string GetEnumDescription<T>(T value, Type enumType)
        {
            var fi = enumType.GetField(value.ToString());

            var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>().FirstOrDefault();

            return attribute != null ? attribute.Description : value.ToString();
        }

        public static string GetEnumDescription<T>(this T value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static T GetValueFromDescription<T>(this string description)
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    if (attribute.Description.Equals(description))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.Equals(description))
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", "description"); // or return default(T);
        }

        public static IEnumerable<Enum> GetValues(Enum enumeration)
        {
            return enumeration.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fieldInfo => (Enum)fieldInfo.GetValue(enumeration)).ToList();
        }

        public static string GetName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }

        /// <summary>
        /// Convert string to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumString"></param>
        /// <returns></returns>
        public static T ToEnums<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString, true);
        }

        /// <summary>
        /// Convert string to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"> </param>
        /// <returns></returns>
        public static T ToEnums<T>(this int enumValue)
        {
            return (T)(object)enumValue;
        }
    }
}
