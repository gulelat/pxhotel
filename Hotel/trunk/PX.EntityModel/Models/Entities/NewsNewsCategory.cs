using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(NewsNewsCategoryMetaData))]
    [Table(Name = "NewsNewsCategories")]
    public partial class NewsNewsCategory
    {
    }

    public class NewsNewsCategoryMetaData
    {
    }
}
