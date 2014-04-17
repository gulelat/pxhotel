using System.Reflection;
using System.Web.Mvc;
using PX.Core.Logging;

namespace PX.Business.Mvc.Attributes.ErrorHandle
{
    public class PxHandleErrorAttribute : HandleErrorAttribute
    {
        private static readonly ILogger Logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            Logger.Error(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}
