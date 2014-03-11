using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.EntityModel;
using PX.EntityModel.Resources;
using PX.Library.Configuration;

namespace PX.Business.Sercurity.Attributes
{

    public class PxAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext authorizationContext)
        {
            base.OnAuthorization(authorizationContext);

            //If its an unauthorized/timed out ajax request go to top window and redirect to logon.
            if (authorizationContext.Result is HttpUnauthorizedResult && authorizationContext.HttpContext.Request.IsAjaxRequest())
            {
                authorizationContext.Controller.TempData["ErrorMessage"] = SystemResources.UnauthorizedAccessMessage;

                authorizationContext.Result = new JavaScriptResult { Script = "top.location = '/';" };
            }

            //If authorization results in HttpUnauthorizedResult, redirect to error page instead of Logon page.
            if (authorizationContext.Result is HttpUnauthorizedResult)
            {
                authorizationContext.Controller.TempData["ErrorMessage"] = SystemResources.UnauthorizedAccessMessage;
                authorizationContext.Result = 
                    authorizationContext.HttpContext.Request.Path.ToLower().Contains("admin") ? 
                        new RedirectResult(Configurations.AdminLoginPagePath + "?returnUrl=" + authorizationContext.HttpContext.Request.Path) 
                        : new RedirectResult(Configurations.LoginPagePath + "?returnUrl=" + authorizationContext.HttpContext.Request.Path);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            //if (!httpContext.User.Identity.IsAuthenticated)
            //    return false;
            var currentUser = User.CurrentUser;

            if (currentUser == null)
            {
                FormsAuthentication.SignOut();
                return false;
            }

            var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["actionName"].ToString();

            return true;

            //return AuthorizedRoles.Any(ut => ut == currentUser.RoleId);
        }
    }
}
