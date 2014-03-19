using System;
using System.Configuration;
using PX.Core.Configurations.Constants;

namespace PX.Core.Configurations
{

    public static class Configurations
    {
        #region News Configurations
        /// <summary>
        /// Gets the page size of News.
        /// </summary>
        /// <remarks></remarks>
        public static int PageSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSizes"]); }
        }

        /// <summary>
        /// Gets the default news image.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultNewsImage
        {
            get { return ConfigurationManager.AppSettings["DefaultNewsImage"]; }
        }

        /// <summary>
        /// Gets the default news save folder.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultNewsFolder
        {
            get { return DefaultConstants.DefaultNewsFolder; }
        }

        /// <summary>
        /// Gets the number of last News.
        /// </summary>
        /// <remarks></remarks>
        public static int LastNewsPageSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["LastNewsPageSizes"]); }
        }

        /// <summary>
        /// Gets the page size of News.
        /// </summary>
        /// <remarks></remarks>
        public static int NewsPageSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["NewsPageSizes"]); }
        }

        /// <summary>
        /// Gets the default ratio of news thumbnail.
        /// </summary>
        /// <remarks></remarks>
        public static double DefaultNewsRatio
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["DefaultNewsRatio"]); }
        }
        #endregion

        #region Parner Configurations

        /// <summary>
        /// Gets the default partner image.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultPartnerImage
        {
            get { return ConfigurationManager.AppSettings["DefaultPartnerImage"]; }
        }

        /// <summary>
        /// Gets the default partner save folder.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultPartnerFolder
        {
            get { return DefaultConstants.DefaultPartnerFolder; }
        }

        /// <summary>
        /// Gets the page size of partners.
        /// </summary>
        /// <remarks></remarks>
        public static int PartnerPageSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PartnerPageSizes"]); }
        }

        /// <summary>
        /// Gets the default ratio of partner thumbnail.
        /// </summary>
        /// <remarks></remarks>
        public static double DefaultPartnerRatio
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["DefaultPartnerRatio"]); }
        }
        #endregion

        #region Class Configurations

        /// <summary>
        /// Gets the default class image.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultClassImage
        {
            get { return ConfigurationManager.AppSettings["DefaultClassImage"]; }
        }

        /// <summary>
        /// Gets the default class save folder.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultClassFolder
        {
            get { return DefaultConstants.DefaultClassFolder; }
        }

        /// <summary>
        /// Gets the page size of classes.
        /// </summary>
        /// <remarks></remarks>
        public static int ClassPageSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ClassPageSizes"]); }
        }

        /// <summary>
        /// Gets the default ratio of class thumbnail.
        /// </summary>
        /// <remarks></remarks>
        public static double DefaultClassRatio
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["DefaultClassRatio"]); }
        }
        #endregion

        #region Service Configurations

        /// <summary>
        /// Gets the default Service image.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultServiceImage
        {
            get { return ConfigurationManager.AppSettings["DefaultServiceImage"]; }
        }

        /// <summary>
        /// Gets the default Service save folder.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultServiceFolder
        {
            get { return DefaultConstants.DefaultServiceFolder; }
        }

        /// <summary>
        /// Gets the page size of Services.
        /// </summary>
        /// <remarks></remarks>
        public static int ServicePageSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ServicePageSizes"]); }
        }

        /// <summary>
        /// Gets the default ratio of Service thumbnail.
        /// </summary>
        /// <remarks></remarks>
        public static double DefaultServiceRatio
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["DefaultServiceRatio"]); }
        }
        #endregion

        /// <summary>
        /// Gets the page cut lenght of description.
        /// </summary>
        /// <remarks></remarks>
        public static int DescriptionLength
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DescriptionLength"]); }
        }

        /// <summary>
        /// Gets the page cut height of thumbnail.
        /// </summary>
        /// <remarks></remarks>
        public static int ThumbnailHeight
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ThumbnailHeight"]); }
        }

        #region Mail Configurations
        /// <summary>
        /// The path to the mail from.
        /// </summary>
        public static string MailFrom
        {
            get { return ConfigurationManager.AppSettings["MailFrom"]; }
        }

        /// <summary>
        /// The path to the mail to.
        /// </summary>
        public static string MailTo
        {
            get { return ConfigurationManager.AppSettings["MailTo"]; }
        }

        #endregion

        #region Common Configurations
        /// <summary>
        /// The path to the site's login form.
        /// </summary>
        public static string LoginPagePath
        {
            get { return ConfigurationManager.AppSettings["LoginPagePath"]; }
        }

        /// <summary>
        /// The folder to save upload image.
        /// </summary>
        public static string UploadFolder
        {
            get { return DefaultConstants.UploadFolder; }
        }

        /// <summary>
        /// The temp folder to save image.
        /// </summary>
        public static string TempFolder
        {
            get { return DefaultConstants.TempFolder; }
        }

        /// <summary>
        /// The path to the admin site's login form.
        /// </summary>
        public static string AdminLoginPagePath
        {
            get { return ConfigurationManager.AppSettings["AdminLoginPagePath"]; }
        }

        /// <summary>
        /// The path to the server push camera url.
        /// </summary>
        public static string ServerPushUrl
        {
            get { return "http://{0}:{1}/video.cgi?user={2}&pwd={3}&"; }
        }

        /// <summary>
        /// The path to the snap shot camera url.
        /// </summary>
        public static string SnapShotUrl
        {
            get { return "http://{0}:{1}/snapshot.cgi?user={2}&pwd={3}&"; }
        }

        /// <summary>
        /// Gets the page size of reference News.
        /// </summary>
        /// <remarks></remarks>
        public static int ReferenceSizes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ReferenceSizes"]); }
        }
        #endregion

        #region Admin Configurations
        /// <summary>
        /// Gets the page size of admin list.
        /// </summary>
        /// <remarks></remarks>
        public static int AdminPageSizes
        {
            get { return 10; }
        }
        #endregion

        #region User Configurations

        /// <summary>
        /// Gets the default user image.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultUserImage
        {
            get { return ConfigurationManager.AppSettings["DefaultUserImage"]; }
        }

        /// <summary>
        /// Gets the default user save folder.
        /// </summary>
        /// <remarks></remarks>
        public static string DefaultUserFolder
        {
            get { return DefaultConstants.DefaultUserFolder; }
        }

        /// <summary>
        /// Gets the default ratio of user avatar.
        /// </summary>
        /// <remarks></remarks>
        public static double DefaultUserRatio
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["DefaultUserRatio"]); }
        }

        /// <summary>
        /// Gets the timeout viewing camera in minutes.
        /// </summary>
        /// <remarks></remarks>
        public static double ViewCameraTimeOut
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ViewCameraTimeOut"]) * 60 * 1000; }
        }

        /// <summary>
        /// Gets the timeout viewing camera in second.
        /// </summary>
        /// <remarks></remarks>
        public static double HearBeatInterval
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["HearBeatInterval"]) * 1000; }
        }
        #endregion

        #region Account Configurations
        /// <summary>
        /// Gets the price per month.
        /// </summary>
        /// <remarks></remarks>
        public static int PricePerMonth
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PricePerMonth"]); }
        }

        /// <summary>
        /// Gets the date starting count.
        /// </summary>
        /// <remarks></remarks>
        public static int DayCount
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DayCount"]); }
        }
        #endregion
        
    }
}
