using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(NewMetaData))]
    [Table(Name = "News")]
    public partial class New
    {
    }

    public class NewMetaData
    {
    }
}
