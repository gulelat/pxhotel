﻿using PX.Core.Framework.Enums;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.PageAudits
{
    public class PageAuditModel : BaseModel
    {
        #region Public Properties
        public int Id { get; set; }

        public int PageId { get; set; }

        public string Title { get; set; }

        public int? FileTemplateId { get; set; }

        public string FileTemplateName { get; set; }

        public int? PageTemplateId { get; set; }

        public string PageTemplateName { get; set; }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumUtilities.GetName((PageEnums.PageStatusEnums)Status);
            }
            set
            {
                Status = value.ToInt();
            }
        }

        public bool IsHomePage { get; set; }

        public string FriendlyUrl { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        #endregion

        public PageAuditModel()
        {
            
        }

        public PageAuditModel(Page page)
        {
            PageId = page.Id;
            Title = page.Title;
            FileTemplateId = page.FileTemplateId;
            FileTemplateName = page.FileTemplateId.HasValue ? page.FileTemplate.Name : string.Empty;
            PageTemplateId = page.PageTemplateId;
            PageTemplateName = page.PageTemplateId.HasValue ?  page.PageTemplate.Name : string.Empty;
            Status = page.Status;
            FriendlyUrl = page.FriendlyUrl;
            ParentId = page.ParentId;
            ParentName = page.ParentId.HasValue ? page.Page1.Title : string.Empty;
        }
    }
}
