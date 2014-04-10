using System.ComponentModel;

namespace PX.Core.Framework.Enums
{
    public enum PermissionEnums
    {
        [Description("Admin area. Manage News")]
        ManageNews = 1,

        [Description("Admin area. Manage User")]
        ManageUser = 2,

        [Description("Admin area. Manage Hotel")]
        ManageHotel = 3,

        [Description("Admin area. Manage content")]
        ManageContent = 4
    }
}
