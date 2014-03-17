using System;
using System.ComponentModel.DataAnnotations;

namespace PX.EntityModel.Models.DTO
{
    public class MenuModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Hierarchy { get; set; }
        
        [Required]
        public string MenuClass { get; set; }

        public int? RecordOrder { get; set; }

        public bool? RecordActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdatedBy { get; set; }
    }
}
