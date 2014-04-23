using System;
using PX.EntityModel;

namespace PX.Business.Models.PageTemplateLogs
{
    public class PageTemplateLogViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        public DateTime Created { get; set; }

        public User Creator { get; set; }
    }
}
