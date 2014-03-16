using System.Collections.Generic;

namespace PX.Core.Configurations
{
    public class Constants
    {
        public const int DefaultAdmin = 1;
        public const string DefaultPassword = "condanglamgi";

        public const string DefaultUserFolder = "~/Images/users/";
        public const string DefaultPartnerFolder = "~/Images/partners/";
        public const string DefaultServiceFolder = "~/Images/partners/";
        public const string DefaultClassFolder = "~/Images/classes/";
        public const string DefaultNewsFolder = "~/Images/news/";
        public const string TempFolder = "~/Images/temp/";
        public const string UploadFolder = "~/Images/upload/";

        public const string CdlgCurrentController = "CDLGCurrentController";
        public const string TempErrorMessage = "ErrorMessage";
        public const string TempStatusMessage = "StatusMessage";

        public const int ThumbnailWidth = 370;

        public static List<string> ImageExtensions = new List<string> { ".jpg", ".jpe", ".bmp", ".gif", ".png" };
    }
}
