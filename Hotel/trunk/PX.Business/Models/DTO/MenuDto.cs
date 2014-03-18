﻿using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.DTO
{
    public class MenuDTO : BaseDTO
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
        
        public string MenuIcon { get; set; }
    }
}
