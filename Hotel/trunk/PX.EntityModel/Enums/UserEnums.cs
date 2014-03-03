using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.EntityModel.Enums
{
    public class UserEnums
    {
        public enum UserTypesEnums
        {
            Admin = 1,
            Moderator = 2,
            CameraManager = 6,
            SchoolManager = 3,
            Teacher = 4,
            Parent = 5
        }


        public enum UserStatusEnums
        {
            Active = StatusEnums.UserActive,
            Inactive = StatusEnums.UserInactive,
            Locked = StatusEnums.UserLocked
        }
    }
}
