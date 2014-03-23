using System.Web;
using PX.EntityModel;

namespace PX.Business.Mvc.WorkContext
{
    public static class WorkContext
    {
        /// <summary>
        /// Get current session cuture
        /// </summary>
        public static string CurrentCuture
        {
            get { return (string)HttpContext.Current.Session["CurrentCuture"]; }
            set { HttpContext.Current.Session["CurrentCuture"] = value; }
        }

        /// <summary>
        /// Get current user stored in cuture
        /// </summary>
        public static User CurrentUser
        {
            get { return (User)HttpContext.Current.Session["CurrentUser"]; }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }
    }
}
