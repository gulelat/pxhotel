using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Routing;

namespace PX.Library.Common
{
    public static class StringUtilities
    {
        private static readonly IList<string> Unpluralizables = new List<string> { "equipment", "information", "rice", "money", "species", "series", "fish", "sheep", "deer" };

        private static readonly IDictionary<string, string> Pluralizations = new Dictionary<string, string>
                                                                                 {
                                                                                     // Start with the rarest cases, and move to the most common
                                                                                     { "person", "people" },
                                                                                     { "ox", "oxen" },
                                                                                     { "child", "children" },
                                                                                     { "foot", "feet" },
                                                                                     { "tooth", "teeth" },
                                                                                     { "goose", "geese" },
                                                                                     // And now the more standard rules.
                                                                                     { "(.*)fe?", "$1ves" },         // ie, wolf, wife
                                                                                     { "(.*)man$", "$1men" },
                                                                                     { "(.+[aeiou]y)$", "$1s" },
                                                                                     { "(.+[^aeiou])y$", "$1ies" },
                                                                                     { "(.+z)$", "$1zes" },
                                                                                     { "([m|l])ouse$", "$1ice" },
                                                                                     { "(.+)(e|i)x$", @"$1ices"},    // ie, Matrix, Index
                                                                                     { "(octop|vir)us$", "$1i"},
                                                                                     { "(.+(s|x|sh|ch))$", @"$1es"},
                                                                                     { "(.+)", @"$1s" }
                                                                                 };

        public static string Pluralize(this string singular)
        {
            if (Unpluralizables.Contains(singular))
                return singular;

            var plural = "";

            foreach (var pluralization in Pluralizations)
            {
                if (Regex.IsMatch(singular, pluralization.Key))
                {
                    plural = Regex.Replace(singular, pluralization.Key, pluralization.Value);
                    break;
                }
            }

            return plural;
        }

        public static string CamelFriendly(this string camel)
        {
            if (String.IsNullOrWhiteSpace(camel))
                return "";

            var sb = new StringBuilder(camel);

            for (var i = camel.Length - 1; i > 0; i--)
            {
                var current = sb[i];
                if ('A' <= current && current <= 'Z')
                {
                    sb.Insert(i, ' ');
                }
            }

            return sb.ToString();
        }

        public static string RemoveTags(this string html)
        {
            if (String.IsNullOrEmpty(html))
            {
                return String.Empty;
            }

            var result = new char[html.Length];

            var cursor = 0;
            var inside = false;
            foreach (var current in html)
            {
                switch (current)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (!inside)
                {
                    result[cursor++] = current;
                }
            }

            return new string(result, 0, cursor);
        }

        public static string FormatWith(this string s, params object[] parms)
        {
            return String.Format(s, parms);
        }

        public static string GetBasePathFromCurrentPath(string folderNameToGet, string currentPath, string baseFolderName)
        {
            if (String.IsNullOrEmpty(currentPath))
            {
                return null;
            }
            string basePath = null;
            var viewsPartIndex = currentPath.LastIndexOf(@"\" + baseFolderName, StringComparison.OrdinalIgnoreCase);
            if (viewsPartIndex >= 0)
            {
                basePath = currentPath.Substring(0, viewsPartIndex + 1) + folderNameToGet;
            }

            var appPath = AppDomain.CurrentDomain.BaseDirectory;

            return basePath;
        }

        public static string StripHtml(this string inputString)
        {
            return Regex.Replace(inputString, "<.*?>", string.Empty);
        }

        public static string SafeSubstring(this string input, int length)
        {
            if (length > input.Length)
            {
                return input;
            }

            var endPosition = input.IndexOf(" ", length, StringComparison.Ordinal);
            if (endPosition < 0) endPosition = input.Length;

            return length >= input.Length ? input : input.Substring(0, endPosition) + "...";
        }

        public static string RemoveDiacritics(this string name)
        {
            var stFormD = name.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var t in from t in stFormD
                              let uc = CharUnicodeInfo.GetUnicodeCategory(t)
                              where uc != UnicodeCategory.NonSpacingMark
                              select t)
            {
                sb.Append(t);
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string Slugify(this string input)
        {
            var disallowed = new Regex(@"[/:?#\[\]@!$&'()*+,.;=\s\""\<\>\\\|%]+");

            var cleanedSlug = disallowed.Replace(input, "-").Trim('-', '.');

            var slug = Regex.Replace(cleanedSlug, @"\-{2,}", "-");

            if (slug.Length > 1000)
                slug = slug.Substring(0, 1000).Trim('-', '.');

            slug = slug.RemoveDiacritics();
            return slug;
        }

        public static string RemoveDoubleSpace(this string input)
        {
            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }
            return input;
        }

        public static Dictionary<string, string> ToDictionary(this string s)
        {
            var dictionary = new Dictionary<string, string>();
            var keyValuePairs = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in keyValuePairs)
            {
                var values = pair.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != 2)
                    continue;

                var key = values[0];
                var value = values[1];

                dictionary[key.ToLower()] = value;
            }
            return dictionary;
        }

        public static RouteValueDictionary ToRouteValueDictionary(this string s)
        {
            var dictionary = s.ToDictionary();

            var routeDictionary = new RouteValueDictionary();

            foreach (var routeValue in dictionary)
            {
                var key = routeValue.Key;
                routeDictionary.Add(key.EndsWith("-")
                                    ? key.Substring(0, key.Length - 1)
                                    : key, routeValue.Value);
            }

            return routeDictionary;
        }

        /// <summary>
        /// Convert a dictionary to aggregated string format like key=value;key2=value2;...;keyn=valuen
        /// </summary>
        /// <param name="dictionary">The input dictionary</param>
        /// <returns>Aggregated string in format key=value;key2=value2;...;keyn=valuen</returns>
        public static string ToAggregatedString(this Dictionary<string, string> dictionary)
        {
            var str = dictionary.Keys.Aggregate("", (current, dictionaryKey) => current + (dictionaryKey + "=" + dictionary[dictionaryKey] + ";"));
            return str;
        }

        public static string ReplaceOnce(this string template, string placeholder, string replacement)
        {
            var loc = template.IndexOf(placeholder, StringComparison.Ordinal);
            if (loc < 0)
            {
                return template;
            }
            return new StringBuilder(template.Substring(0, loc))
                .Append(replacement)
                .Append(template.Substring(loc + placeholder.Length))
                .ToString();
        }

        public static string LastPart(this string input, char separator)
        {
            var pos = input.LastIndexOf(separator);
            if (pos < 0)
            {
                return input;
            }
            if (pos == input.Length - 1)
            {
                return "";
            }
            return input.Substring(pos + 1);
        }
    }

    public class StringLengthComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var result = -(x.Length.CompareTo(y.Length));
            if (result == 0)
            {
                result = String.Compare(x, y, StringComparison.Ordinal);
            }
            return result;
        }
    }
}
