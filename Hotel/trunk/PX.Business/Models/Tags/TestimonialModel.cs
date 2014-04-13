using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Tags
{
    public class TagModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
