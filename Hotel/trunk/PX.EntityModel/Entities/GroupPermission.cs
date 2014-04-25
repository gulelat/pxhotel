using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(GroupPermissionMetaData))]
    [Table(Name = "GroupPermissions")]
    public partial class GroupPermission
    {
    }

    public class GroupPermissionMetaData
    {
    }
}
