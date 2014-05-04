using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.NewsCategories
{
    public class NewsCategoryModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public int? ParentId { get; set; }

        public string ParentName { get; set; }

    }
}
