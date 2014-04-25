using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(NewsCategoryMetaData))]
    [Table(Name = "NewsCategories")]
    public partial class NewsCategory
    {
    }

    public class NewsCategoryMetaData
    {
    }
}
