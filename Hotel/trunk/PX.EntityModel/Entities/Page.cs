using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(PageMetaData))]
    [Table(Name = "Pages")]
    public partial class Page
    {
    }

    public class PageMetaData
    {
    }
}
