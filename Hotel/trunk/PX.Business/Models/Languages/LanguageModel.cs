using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Languages
{
    public class LanguageModel : BaseModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
