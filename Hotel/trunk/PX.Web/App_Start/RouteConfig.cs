using System.Web.Mvc;
using System.Web.Routing;

namespace PX.Web
{
    public class RouteConfig
    {
        public static string NameSpaces
        {
            get
            {
                return "PX.Web.Controllers";
            }
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DefaultLocalized",
                "{language}_{culture}/{controller}/{action}/{id}",
                new
                {
                    id = UrlParameter.Optional,
                    languageKey = "en",
                    culture = "US"
                },
                new[] { NameSpaces });

            routes.MapRoute(
                "DefaultLocalizedWithFriendlyUrl",
                "{language}_{culture}/",
                new
                {
                    controller = "Home",
                    action = "Index",
                    url = UrlParameter.Optional,
                    languageKey = "en",
                    culture = "US"
                },
                new[] { NameSpaces });

            routes.MapRoute(
                "EmptyLocalized",
                "{language}_{culture}/{*url}",
                new
                {
                    controller = "Pages",
                    action = "Index",
                    url = UrlParameter.Optional,
                    languageKey = "en",
                    culture = "US"
                },
                new[] { NameSpaces });

            //Default route
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                    {
                        id = UrlParameter.Optional
                    },
                new[] { NameSpaces }
                );

            //Empty route for home page
            routes.MapRoute(
                "Empty",
                "",
                new
                {
                    controller = "Pages",
                    action = "Index",
                    url = ""
                },
                new[] { NameSpaces });

            routes.MapRoute(
                "FriendlyUrl",
                "{*url}",
                new
                {
                    controller = "Pages",
                    action = "Index",
                    url = UrlParameter.Optional
                },
                new[] { NameSpaces });
        }
    }
}