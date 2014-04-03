using System.Collections.Generic;
using System.Web.Mvc;

namespace PX.Business.Models.RotatingImages
{
    public class RotatingImageManageModel
    {
        public int? Id { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public int GroupId { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }

        public int RecordOrder { get; set; }
    }
}
