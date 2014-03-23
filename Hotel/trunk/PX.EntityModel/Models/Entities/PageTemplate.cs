using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(PageTemplateMetaData))]
    [Table(Name = "PageTemplates")]
    public partial class PageTemplate
    {
    }

    public class PageTemplateMetaData
    {
    }
}
