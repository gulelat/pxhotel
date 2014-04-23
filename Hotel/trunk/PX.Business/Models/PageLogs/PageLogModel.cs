using System;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.PageLogs
{
    public class PageLogModel
    {
        #region Constructors
        public PageLogModel()
        {

        }

        public PageLogModel(PageLog pageLog)
        {
            PageId = pageLog.Id;
            Title = pageLog.Title;
            FileTemplateId = pageLog.FileTemplateId;
            PageTemplateId = pageLog.Page.PageTemplateId;
            Status = pageLog.Status;
            FriendlyUrl = pageLog.FriendlyUrl;
            ParentId = pageLog.ParentId;
        }

        public PageLogModel(Page page)
        {
            PageId = page.Id;
            Title = page.Title;
            FileTemplateId = page.FileTemplateId;
            PageTemplateId = page.PageTemplateId;
            Status = page.Status;
            FriendlyUrl = page.FriendlyUrl;
            ParentId = page.ParentId;
        }
        #endregion

        #region Public Properties
        public int Id { get; set; }

        public int PageId { get; set; }

        public string Title { get; set; }

        public string ChangeLog { get; set; }

        public int? FileTemplateId { get; set; }

        public int? PageTemplateId { get; set; }

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

        [DefaultOrder]
        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        #endregion
    }
}
