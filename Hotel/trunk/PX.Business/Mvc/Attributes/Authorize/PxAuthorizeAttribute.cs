using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;

namespace PX.Business.Mvc.Attributes.Authorize
{
    public class PxAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Permission array
        /// </summary>
        public PermissionEnums[] Permissions { get; set; }

        /// <summary>
        /// On authorizing
        /// </summary>
        /// <param name="authorizationContext">the authorize context</param>
        public override void OnAuthorization(AuthorizationContext authorizationContext)
        {
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            base.OnAuthorization(authorizationContext);

            var urlHelper = new UrlHelper(authorizationContext.RequestContext);

            if (authorizationContext.Result is HttpUnauthorizedResult)
            {
                authorizationContext.Controller.TempData["ErrorMessage"] = localizedResourceServices.T("AdminModule:::GroupPermissions:::You don't have permission to access this featured. Please log in");
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

        /// <summary>
        /// Authorize
        /// </summary>
        /// <param name="httpContext">the current context</param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var isAuthorize = base.AuthorizeCore(httpContext);
            if (!isAuthorize)
            {
                return false;
            }

            var userServices = HostContainer.GetInstance<IUserServices>();
            var currentUser = userServices.GetActiveUser(httpContext.User.Identity.Name);
            if(currentUser == null)
            {
                FormsAuthentication.SignOut();
                return false;
            }
            if (WorkContext.WorkContext.CurrentUser == null)
            {
                currentUser.LastLogin = DateTime.Now;
                userServices.Update(currentUser);
                WorkContext.WorkContext.CurrentUser = currentUser;
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
