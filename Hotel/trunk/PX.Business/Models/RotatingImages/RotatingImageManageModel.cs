using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PX.Business.Models.RotatingImages
{
    public class RotatingImageManageModel
    {
        public int? Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Text { get; set; }
        
        [Required]
        public string Url { get; set; }

        [Required]
        public int GroupId { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }

        public int RecordOrder { get; set; }
    }
}
