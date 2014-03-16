namespace PX.Core.Framework.Enums
{
    public class UserEnums
    {
        public enum UserTypesEnums
        {
            Admin = 1,
            Moderator = 2,
            Customer = 3
        }


        public enum UserStatusEnums
        {
            Active = StatusEnums.UserActive,
            Inactive = StatusEnums.UserInactive,
            Locked = StatusEnums.UserLocked
        }
    }
}
