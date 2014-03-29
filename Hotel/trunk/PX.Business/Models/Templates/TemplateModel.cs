using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Templates
{
    public class TemplateModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DataType { get; set; }
    }
}
