using System.Threading;
using System.Web.Mvc;
using PX.Business.Mvc.Environments;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.Languages;

namespace PX.Business.Mvc.Attributes
{
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var languageServices = HostContainer.GetInstance<ILanguageServices>();
            var language = (string)filterContext.RouteData.Values["language"] ?? string.Empty;
            var culture = (string)filterContext.RouteData.Values["culture"] ?? string.Empty;

            if (string.IsNullOrEmpty(language) && string.IsNullOrEmpty(culture))
            {
                if (string.IsNullOrEmpty(WorkContext.WorkContext.CurrentCuture))
                {
                    var threadCuture = Thread.CurrentThread.CurrentCulture.Name;
                    var country = languageServices.GetById(threadCuture);
                    if (country != null)
                    {
                        WorkContext.WorkContext.CurrentCuture = threadCuture;
                    }
                }
            }
            else
            {
                var currentLanguage = languageServices.GetById(string.Format("{0}-{1}", language, culture));
                if (currentLanguage != null)
                {
                    WorkContext.WorkContext.CurrentCuture = currentLanguage.Id;
                }
                else
                {
                    filterContext.Result = new HttpNotFoundResult();
                }
            }
        }
    }
}