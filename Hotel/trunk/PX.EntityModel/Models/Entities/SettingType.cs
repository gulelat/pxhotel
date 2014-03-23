using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(SettingTypeMetaData))]
    [Table(Name = "SettingTypes")]
    public partial class SettingType
    {
    }

    public class SettingTypeMetaData
    {
    }
}
