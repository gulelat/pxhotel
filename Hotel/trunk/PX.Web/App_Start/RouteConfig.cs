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
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { NameSpaces }
                );
        }
    }
}