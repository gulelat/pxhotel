using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.LocalizedResources
{
    public class LocalizedResourceModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string LanguageId { get; set; }

        [Required]
        public string TextKey { get; set; }

        public string DefaultValue { get; set; }

        public string TranslatedValue { get; set; }
    }
}
