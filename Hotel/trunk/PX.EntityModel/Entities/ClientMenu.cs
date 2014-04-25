using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(ClientMenuMetaData))]
    [Table(Name = "ClientMenus")]
    public partial class ClientMenu
    {
        public ClientMenu(Page page)
        {
            Name = page.Title;
            PageId = page.Id;
            Url = page.FriendlyUrl;
            if (page.ParentId.HasValue && page.Page1.ClientMenus.Any())
            {
                ParentId = page.Page1.ClientMenus.First().Id;
            }
            else
            {
                ParentId = null;                
            }
            IncludeInSiteNavigation = page.IncludeInSiteNavigation;
            StartPublishingDate = page.StartPublishingDate;
            EndPublishingDate = page.EndPublishingDate;
            RecordOrder = page.RecordOrder * 10;
            RecordActive = page.RecordActive;
        }
    }

    public class ClientMenuMetaData
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? PageId { get; set; }

        public string Url { get; set; }

        public int? ParentId { get; set; }

        public string Hierarchy { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }
        
        public int? RecordOrder { get; set; }

        public bool RecordActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdatedBy { get; set; }
    }
}
