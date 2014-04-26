using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using PX.Core.Configurations;

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

        public string AvatarPath
        {
            get
            {
                if (string.IsNullOrEmpty(AuthorImageUrl) || !File.Exists(HttpContext.Current.Server.MapPath(AuthorImageUrl)))
                {
                    return Configurations.AvatarFolder + Configurations.NoAvatar;
                }
                return AuthorImageUrl;
            }
        }
    }
}
