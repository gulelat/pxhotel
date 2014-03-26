using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Testimonials
{
    public class NewsCategoryModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageFileName { get; set; }

    }
}
