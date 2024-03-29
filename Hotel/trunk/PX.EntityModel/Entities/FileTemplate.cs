﻿using System;
using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Attributes;

namespace PX.EntityModel
{
    [MetadataType(typeof(FileTemplateMetaData))]
    [Table(Name = "FileTemplates")]
    public partial class FileTemplate
    {
    }

    public class FileTemplateMetaData
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

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
