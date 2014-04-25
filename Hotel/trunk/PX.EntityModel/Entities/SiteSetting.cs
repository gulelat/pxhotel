using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(SiteSettingMetaData))]
    [Table(Name = "SiteSettings")]
    public partial class SiteSetting
    {
    }

    public class SiteSettingMetaData
    {
    }
}
