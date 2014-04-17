using System;
using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(ClientMenuMetaData))]
    [Table(Name = "ClientMenus")]
    public partial class ClientMenu
    {
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
        
        public int? RecordOrder { get; set; }

        public bool RecordActive { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdatedBy { get; set; }
    }
}
