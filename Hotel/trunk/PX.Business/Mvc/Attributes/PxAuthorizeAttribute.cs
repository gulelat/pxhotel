using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Environment;
using PX.EntityModel;
using PX.EntityModel.Resources;

namespace PX.Business.Mvc.Attributes
{
    public class PxAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionEnums[] Permissions { get; set; }

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
                    authorizationContext.Result = new JavaScriptResult { Script = string.Format("top.location = {0};", urlHelper.Action("Login", "Account", new { area = "Admin", returnUrl = authorizationContext.HttpContext.Request.Path })) };
                }
                //If it's a child action, return 404 result
                else if (authorizationContext.Controller.ControllerContext.IsChildAction)
                {
                    authorizationContext.Result = new HttpNotFoundResult();
                }
                // Redirect to login page
                else
                {
                    authorizationContext.Result = new RedirectResult(urlHelper.Action("Login", "Account", new { area = "Admin", returnUrl = authorizationContext.HttpContext.Request.Path }));
                }

            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var isAuthorize = base.AuthorizeCore(httpContext);
            if (!isAuthorize)
            {
                return false;
            }

            var currentUser = User.CurrentUser;
            if (currentUser == null)
            {
                var userServices = DependencyFactory.GetInstance<IUserServices>();
                currentUser = userServices.GetUser(httpContext.User.Identity.Name);
                if (currentUser != null)
                {
                    currentUser.LastLogin = DateTime.Now;
                    userServices.Update(currentUser);
                    User.CurrentUser = currentUser;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    return false;
                }
            }
            if (Permissions == null || !Permissions.Any())
            {
                return true;
            }
            var permissions = Permissions.Select(p => (int) p);
            var userPermissions = currentUser.UserGroup.GroupPermissions.Where(p => p.HasPermission).Select(p => p.PermissionId).ToList();
            return userPermissions.Intersect(permissions).Count() == Permissions.Count();
        }
    }
}
