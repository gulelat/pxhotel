using System;
using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.ClientMenus
{
    public class ClientMenuModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsPageMenu { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public string Hierarchy { get; set; }

        [Required]
        public bool Visible { get; set; }

        public string VisibleString { get { return Visible ? "Yes" : "No"; } set { Visible = value.Equals("Yes"); } }
    }
}
