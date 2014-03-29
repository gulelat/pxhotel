using System.Collections.Generic;
using System.Web;
using PX.Business.Models.CurlyBrackets;
using PX.Business.Models.LocalizedResources;
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

        /// <summary>
        /// Get all current curly brackets of application
        /// </summary>
        public static IEnumerable<CurlyBracketModel> CurlyBrackets
        {
            get { return (IEnumerable<CurlyBracketModel>)HttpContext.Current.Application["ApplicationCurlyBrackets"]; }
            set { HttpContext.Current.Application["ApplicationCurlyBrackets"] = value; }
        }

        public static IEnumerable<LocalizeDictionaryItem> LocalizedResourceDictionary
        {
            get { return (IEnumerable<LocalizeDictionaryItem>)HttpContext.Current.Application["LocalizedResourceDictionary"]; }
            set { HttpContext.Current.Application["LocalizedResourceDictionary"] = value; }
        }

    }
}
