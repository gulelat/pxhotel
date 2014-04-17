using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PX.Business.Mvc.ViewEngines.ViewResult
{

    public class MVCTransferResult : RedirectResult
    {
        public MVCTransferResult(string url)
            : base(url)
        {
        }

        public MVCTransferResult(object routeValues, string parameters)
            : base(GetRouteURL(routeValues, parameters))
        {
        }

        private static string GetRouteURL(object routeValues, string parameters)
        {
            var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()), RouteTable.Routes);
            var url = urlHelper.RouteUrl(routeValues);
            return string.Format("{0}{1}", url, string.IsNullOrEmpty(parameters) ? string.Empty : string.Format("?{0}", parameters));
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = HttpContext.Current;

            // ASP.NET MVC 3.0
            if (context.Controller.TempData != null &&
                context.Controller.TempData.Any())
            {
                throw new ApplicationException("TempData won't work with Server.TransferRequest!");
            }

            httpContext.Server.TransferRequest(Url, false); // change to false to pass query string parameters if you have already processed them
        }
    }
}
