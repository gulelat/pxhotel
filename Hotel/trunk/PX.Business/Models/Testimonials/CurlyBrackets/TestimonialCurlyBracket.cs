using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Testimonials.CurlyBrackets
{
    public class TestimonialCurlyBracket : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorDescription { get; set; }

        public string AuthorImageUrl { get; set; }
    }
}
