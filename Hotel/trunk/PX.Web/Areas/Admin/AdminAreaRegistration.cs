using System.Web.Mvc;

namespace PX.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }
        public string NameSpaces
        {
            get
            {
                return "PX.Web.Areas.Admin.Controllers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Thumbnail_Handler",
                             "Thumbnail/{id}",
                             new { controller = "Media", action = "Thumbnail", id = UrlParameter.Optional },
                             new[] { NameSpaces }
                );
            context.MapRoute("Admin_default",
                             "Admin/{controller}/{action}/{id}",
                             new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                             new[] { NameSpaces }
                );
        }
    }
}
