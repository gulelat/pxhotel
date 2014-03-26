using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Testimonials
{
    public class NewsModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageFileName { get; set; }

        public int Status { get; set; }
    }
}
