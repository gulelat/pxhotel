using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(CountryMetaData))]
    [Table(Name = "Countries")]
    public partial class Country
    {
    }

    public class CountryMetaData
    {
    }
}
