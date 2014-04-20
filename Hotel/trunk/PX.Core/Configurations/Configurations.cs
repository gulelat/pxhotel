namespace PX.Core.Configurations
{
    public class Configurations
    {
        //Hierarchy
        public const string HierarchyLevelPrefix = "---";

        #region Razor Constants

        public const string RenderBody = "@RenderBody()";

        public const string CurlyBracketRenderBody = "{RenderBody}";

        #endregion

        public const string DefaultSystemAccount = "system";

        #region Media

        public const string NoImage = "~/Images/no-image.png";

        public const string NoAvatar = "noavatar.png";

        public const string AvatarFolder = "/Images/uploads/Avatars/";

        public const string UploadFolder = "~/Images/uploads/";

        #endregion

        #region Data

        public const string DateTimeFormat = "hhmmss-ddMMyyyy";

        public const string DateFormat = "dd/MM/yyyy";

        #endregion

        #region Controller
        public const string PxHotelCurrentController = "PxHotelCurrentController";
        public const string ErrorMessage = "ErrorMessage";
        public const string SuccessMessage = "SuccessMessage";
        public const string WarningMessage = "WarningMessage";

        #endregion
    }
}
