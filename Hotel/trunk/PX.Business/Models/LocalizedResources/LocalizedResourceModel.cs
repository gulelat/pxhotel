using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Localizes
{
    public class LocalizedResourceModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public string Language { get; set; }

        public string TextKey { get; set; }

        public string DefaultValue { get; set; }

        public string TranslatedValue { get; set; }
    }
}
