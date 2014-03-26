using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(NewsMetaData))]
    [Table(Name = "News")]
    public partial class News
    {
    }

    public class NewsMetaData
    {
    }
}
