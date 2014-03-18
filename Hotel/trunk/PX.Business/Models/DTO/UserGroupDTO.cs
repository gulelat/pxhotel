using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.DTO
{
    public class UserGroupDTO : BaseDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
