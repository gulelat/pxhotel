using System.Collections.Generic;

namespace PX.Core.Configurations.Constants
{
    public class DefaultConstants
    {
        public const string DefaultAdmin = "administrator";

        public const string NoAvatar = "noavatar.png";
        public const string AvatarFolder = "/Images/uploads/Avatars/";

        public const string DateFormat = "dd/MM/yyyy";

        public const string DefaultNewsFolder = "~/Images/uploads/news/";

        public const string DefaultUserFolder = "/Images/uploads/Avatars/";
        public const string TempFolder = "~/Images/temp/";
        public const string UploadFolder = "~/Images/upload/";

        public const string CdlgCurrentController = "CDLGCurrentController";
        public const string TempErrorMessage = "ErrorMessage";
        public const string TempStatusMessage = "StatusMessage";

        public const int ThumbnailWidth = 370;

        public static List<string> ImageExtensions = new List<string> { ".jpg", ".jpe", ".bmp", ".gif", ".png" };
    }
}
