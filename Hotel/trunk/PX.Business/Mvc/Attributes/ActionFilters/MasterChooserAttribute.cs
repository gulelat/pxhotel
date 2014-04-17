using System;
using System.Security.Principal;
using System.Web.Mvc;
using PX.Business.Services.FileTemplates;
using PX.Core.Framework.Mvc.Environments;

namespace PX.Business.Mvc.Attributes.ActionFilters
{
    public class MasterChooserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                var fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();
                var controller = filterContext.RouteData.Values["controller"].ToString();
                var action = filterContext.RouteData.Values["action"].ToString();
                var master = fileTemplateServices.GetFileTemplateMaster(controller, action);
                if(!string.IsNullOrEmpty(master))
                {
                    result.MasterName = fileTemplateServices.GetFileTemplateMaster(controller, action);
                }
            }
        }
    }
}
