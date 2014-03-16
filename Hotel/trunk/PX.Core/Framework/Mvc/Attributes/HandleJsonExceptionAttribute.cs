using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PX.Core.Framework.Mvc.Attributes
{
    public class HandleJsonExceptionAttribute : ActionFilterAttribute
    {
        // next class example are modification of the example from
        // the http://www.dotnetcurry.com/ShowArticle.aspx?ID=496
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null)
            {
                filterContext.HttpContext.Response.StatusCode =
                    (int)System.Net.HttpStatusCode.InternalServerError;

                var exInfo = new List<ExceptionInformation>();
                for (Exception ex = filterContext.Exception; ex != null; ex = ex.InnerException)
                {
                    exInfo.Add(new ExceptionInformation
                        {
                        Message = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace
                    });
                }
                filterContext.Result = new JsonResult { Data = exInfo };
                filterContext.ExceptionHandled = true;
            }
        }
    }

    public class ExceptionInformation
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}
