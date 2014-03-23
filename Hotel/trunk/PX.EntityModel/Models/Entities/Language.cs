using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(LanguageTemplateMetaData))]
    [Table(Name = "LanguageTemplates")]
    public partial class LanguageTemplate
    {
    }

    public class LanguageTemplateMetaData
    {
    }
}
