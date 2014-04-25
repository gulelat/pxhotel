using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(UserGroupMetaData))]
    [Table(Name = "UserGroups")]
    public partial class UserGroup
    {
    }

    public class UserGroupMetaData
    {
    }
}
