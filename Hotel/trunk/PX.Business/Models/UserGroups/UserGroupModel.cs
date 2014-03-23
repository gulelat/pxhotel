using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.UserGroups
{
    public class UserGroupModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
