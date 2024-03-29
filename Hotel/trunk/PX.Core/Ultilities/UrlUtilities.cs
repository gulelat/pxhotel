﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace PX.Core.Ultilities
{
    public static class UrlUtilities
    {

        /// <summary>
        /// Generate url from request context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string GenerateUrl(RequestContext context, string controller, string action, object routeValues)
        {
            var urlHelper = new UrlHelper(context);
            return urlHelper.Action(action, controller, routeValues);
        }

        /// <summary>
        /// </summary>
        private static readonly Regex InvalidUrlCharacter = new Regex(@"[^a-z|^_|^\d|^\u4e00-\u9fa5|^/]+",
                                                                      RegexOptions.Compiled | RegexOptions.Singleline |
                                                                      RegexOptions.IgnoreCase);

        public static string UrlSeparatorChar = "/";

        /// <summary>
        /// To the URL string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ToUrlString(this string s)
        {
            if (s == null)
            {
                return null;
            }
            var sb = new StringBuilder(s.Length);
            foreach (var c in s)
            {
                var m = RemapInternationalCharToAscii(c);
                if (string.IsNullOrEmpty(m)) sb.Append(c);
                else sb.Append(m);
            }
            var str = sb.ToString();
            return !string.IsNullOrEmpty(str) ? InvalidUrlCharacter.Replace(str, "-") : str;
        }

        /// <summary>
        /// Remap all international char to Ascii
        /// </summary>
        /// <param name="c">the input string</param>
        /// <returns></returns>
        private static string RemapInternationalCharToAscii(char c)
        {
            var l = Convert.ToString(c).ToLowerInvariant();

            if ("àáạảãäąăååắằặẳẵâấầậẩẩ".Contains(l))
            {
                return "a";
            }
            if ("èéẹẻẽêếềệểễëę".Contains(l))
            {
                return "e";
            }
            if ("ìíịỉĩîïı".Contains(l))
            {
                return "i";
            }
            if ("òóọỏõôồốộổỗơờớợởỡöøőð".Contains(l))
            {
                return "o";
            }
            if ("ùúụủũûüŭůưứừựửữ".Contains(l))
            {
                return "u";
            }
            if ("çćčĉ".Contains(l))
            {
                return "c";
            }
            if ("żźž".Contains(l))
            {
                return "z";
            }
            if ("śşšŝ".Contains(l))
            {
                return "s";
            }
            if ("ñń".Contains(l))
            {
                return "n";
            }
            if ("ýỳỵỷỹÿ".Contains(l))
            {
                return "y";
            }
            if ("ğĝ".Contains(l))
            {
                return "g";
            }
            switch (c)
            {
                case 'ř':
                    return "r";
                case 'ł':
                    return "l";
                case 'đ':
                    return "d";
                case 'ß':
                    return "ss";
                case 'Þ':
                    return "th";
                case 'ĥ':
                    return "h";
                case 'ĵ':
                    return "j";
                default:
                    return "";
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="virtualPaths">
        ///     The virtual paths.
        ///     <example>string[] {"path1","path2","path3"}</example>
        /// </param>
        /// <returns>
        ///     <value>path1/path2/path3</value>
        /// </returns>
        public static string Combine(params string[] virtualPaths)
        {
            if (virtualPaths.Length < 1)
                return null;

            var builder = new StringBuilder();
            for (var i = 0; i < virtualPaths.Length; i++)
            {
                var path = virtualPaths[i];
                if (String.IsNullOrEmpty(path))
                    continue;

                path = path.Replace("\\", "/");

                if (i > 0)
                {
                    // Not first one trim start '/'
                    path = path.TrimStart('/');
                    builder.Append("/");
                }
                if (i < virtualPaths.Length - 1)
                {
                    // Not last one trim end '/'
                    path = path.TrimEnd('/');
                }
                if (!path.Contains('/'))
                {
                    path = Uri.EscapeUriString(path);
                }
                builder.Append(path);
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Return full url start with http.
        /// </summary>
        /// <param name="relativeUrl">Url start with "~"</param>
        /// <returns></returns>
        public static string ToHttpAbsolute(string relativeUrl)
        {
            var url = new UriBuilder(HttpContext.Current.Request.Url);
            var queryIndex = relativeUrl.IndexOf("?", StringComparison.Ordinal);
            if (queryIndex != -1 && queryIndex != relativeUrl.Length)
            {
                url.Query = relativeUrl.Substring(queryIndex + 1);
                relativeUrl = relativeUrl.Substring(0, queryIndex);
            }

            url.Path = VirtualPathUtility.ToAbsolute(relativeUrl);

            return url.Uri.AbsoluteUri;
        }

        /// <summary>
        ///     Toes the HTTP absolute url.
        /// </summary>
        /// <param name="baseUri">The base URI. e.g: http://www.site.com</param>
        /// <param name="url">The URL. e.g: index?q=1</param>
        /// <returns></returns>
        public static string ToHttpAbsolute(string baseUri, string url)
        {
            url = ResolveUrl(url);
            if (string.IsNullOrEmpty(baseUri) || string.IsNullOrEmpty(url))
            {
                return url;
            }
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return url;
            }

            if (HttpContext.Current != null)
            {
                if (!baseUri.StartsWith("http://") && !baseUri.StartsWith("https://"))
                {
                    baseUri = HttpContext.Current.Request.Url.Scheme + "://" + baseUri;
                }
            }

            return new Uri(new Uri(baseUri), url).ToString();
        }

        public static string ToAbsolute(this UrlHelper urlHelper, string action)
        {
            if (urlHelper.RequestContext == null || urlHelper.RequestContext.HttpContext == null
                || urlHelper.RequestContext.HttpContext.Request.Url == null)
                return string.Empty;

            return new Uri(urlHelper.RequestContext.HttpContext.Request.Url,
                           urlHelper.Action(action)).ToString();
        }

        public static string ToAbsolute(this UrlHelper urlHelper, string action, string controllerName)
        {
            if (urlHelper.RequestContext == null || urlHelper.RequestContext.HttpContext == null
                || urlHelper.RequestContext.HttpContext.Request.Url == null)
                return string.Empty;

            return new Uri(urlHelper.RequestContext.HttpContext.Request.Url,
                           urlHelper.Action(action, controllerName)).ToString();
        }

        public static string ToAbsolute(this UrlHelper urlHelper, string action, string controllerName, object routeValues)
        {
            if (urlHelper.RequestContext == null || urlHelper.RequestContext.HttpContext == null
                || urlHelper.RequestContext.HttpContext.Request.Url == null)
                return string.Empty;

            return new Uri(urlHelper.RequestContext.HttpContext.Request.Url,
                           urlHelper.Action(action, controllerName, routeValues)).ToString();
        }

        public static string ToAbsolute(this UrlHelper urlHelper, string action, string controllerName, RouteValueDictionary routeValues)
        {
            if (urlHelper.RequestContext == null || urlHelper.RequestContext.HttpContext == null
                || urlHelper.RequestContext.HttpContext.Request.Url == null)
                return string.Empty;

            return new Uri(urlHelper.RequestContext.HttpContext.Request.Url,
                           urlHelper.Action(action, controllerName, routeValues)).ToString();
        }

        /// <summary>
        ///     Equal to <see cref="System.Web.Mvc.UrlHelper.Content" />  AND <see cref="System.Web.UI.Control.ResolveUrl" />
        ///     <remarks>
        ///         Independent of HttpContext
        ///     </remarks>
        /// </summary>
        /// <param name="relativeUrl">
        ///     The URL.
        ///     <example>~/a/b</example>
        /// </param>
        /// <returns>
        ///     <value>/a/b OR {virtualPath}/a/b</value>
        /// </returns>
        public static string ResolveUrl(string relativeUrl)
        {
            if (!string.IsNullOrEmpty(relativeUrl) && HttpContext.Current != null && relativeUrl.StartsWith("~"))
            {
                //For FrontHttpRequestWrapper
                var applicationPath = HttpContext.Current.Items["ApplicationPath"] != null
                                             ? HttpContext.Current.Items["ApplicationPath"].ToString()
                                             : HttpContext.Current.Request.ApplicationPath;
                return applicationPath == "/" ? relativeUrl.Remove(0, 1) : applicationPath + relativeUrl.Remove(0, 1);
            }
            return relativeUrl;
        }

        /// <summary>
        ///     Wrap <see cref="System.Web.HttpServerUtilityBase.MapPath" />
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Independent of HttpContext
        /// </remarks>
        public static string MapPath(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }
            var physicalPath = HostingEnvironment.MapPath(url);
            if (physicalPath == null)
            {
                physicalPath =
                    url.TrimStart('~').Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);

                physicalPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, physicalPath);
            }
            return physicalPath;
        }

        /// <summary>
        ///     Combines the query string.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="queries">The queries.</param>
        /// <returns></returns>
        public static string CombineQueryString(string baseUrl, params string[] queries)
        {
            var query = String.Join(String.Empty, queries).TrimStart('?');
            if (String.IsNullOrEmpty(query))
            {
                return baseUrl;
            }
            return baseUrl.Contains('?') ? baseUrl + query : baseUrl + "?" + query;
        }

        /// <summary>
        ///     Removes the query.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="names">The names.</param>
        /// <returns></returns>
        public static string RemoveQuery(string url, params string[] names)
        {
            var result = url;
            foreach (var each in names)
            {
                result = ReplaceQuery(url, each, String.Empty);
            }
            return result;
        }

        /// <summary>
        ///     Replaces the query.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="name">The name.</param>
        /// <param name="newQuery">The new query.</param>
        /// <returns></returns>
        public static string ReplaceQuery(string url, string name, string newQuery)
        {
            return Regex.Replace(url, String.Format(@"&?\b{0}=[^&]*", name), newQuery, RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Ensures the HTTP head.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string EnsureHttpHead(string url)
        {
            if (String.IsNullOrEmpty(url))
                return url;

            if (!Regex.IsMatch(url, @"^\w"))
                return url;

            if (url.StartsWith("mailto:"))
                return url;

            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return url;

            return "http://" + url;
        }

        /// <summary>
        ///     Gets the virtual path.
        /// </summary>
        /// <param name="physicalPath">The physical path.</param>
        /// <returns></returns>
        public static string GetVirtualPath(this string physicalPath)
        {
            var rootpath = MapPath("~/");
            physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");
            return "~/" + physicalPath;
        }

        /// <summary>
        ///     Adds the query param.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string AddQueryParam(
            this string source, string key, string value)
        {
            string delim;
            if ((source == null) || !source.Contains("?"))
            {
                delim = "?";
            }
            else if (source.EndsWith("?") || source.EndsWith("&"))
            {
                delim = string.Empty;
            }
            else
            {
                delim = "&";
            }

            return source + delim + HttpUtility.UrlEncode(key)
                   + "=" + HttpUtility.UrlEncode(value);
        }

        /// <summary>
        ///     Determines whether [is absolute URL] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        ///     <c>true</c> if [is absolute URL] [the specified URL]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAbsoluteUrl(string url)
        {
            return (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)) ||
                   url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }
    }
}
