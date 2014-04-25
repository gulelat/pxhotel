using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(LocalizedResourceMetaData))]
    [Table(Name = "LocalizedResources")]
    public partial class LocalizedResource
    {
    }

    public class LocalizedResourceMetaData
    {
    }
}
