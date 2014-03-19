using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Services.Users;
using PX.EntityModel;
using PX.EntityModel.Resources;

namespace PX.Business.Mvc.Attributes
{
    public class PxAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext authorizationContext)
        {
            base.OnAuthorization(authorizationContext);

            var urlHelper = new UrlHelper(authorizationContext.RequestContext);

            if (authorizationContext.Result is HttpUnauthorizedResult)
            {
                authorizationContext.Controller.TempData["ErrorMessage"] = SystemResources.UnauthorizedAccessMessage;
                //If its an unauthorized/timed out ajax request go to top window and redirect to logon.
                if (authorizationContext.HttpContext.Request.IsAjaxRequest())
                {
                    authorizationContext.Result = new JavaScriptResult { Script = string.Format("top.location = {0};", urlHelper.Action("Login", "Account", new { returnUrl = authorizationContext.HttpContext.Request.Path })) };
                }
                else if (authorizationContext.Controller.ControllerContext.IsChildAction)
                {
                    authorizationContext.Result = new HttpNotFoundResult();
                }
                else
                {
                    authorizationContext.Result = new RedirectResult(urlHelper.Action("Login", "Account", new { returnUrl = authorizationContext.HttpContext.Request.Path }));
                }

            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var isAuthorize = base.AuthorizeCore(httpContext);
            if(!isAuthorize)
            {
                return false;
            }

            var currentUser = User.CurrentUser;
            if (currentUser == null)
            {
                var userServices = new UserServices();
                currentUser = userServices.GetUser(httpContext.User.Identity.Name);
                if(currentUser != null)
                {
                    User.CurrentUser = currentUser;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    return false;
                }
            }

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            var actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();

            return true;
        }
    }
}
