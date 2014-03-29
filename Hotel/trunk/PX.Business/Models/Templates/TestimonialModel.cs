using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Testimonials
{
    public class TestimonialModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
