using System;
using System.Linq;
using PX.Core.Framework.Enums;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.PageLogs
{
    public class PageLogPreviewModel : BaseModel
    {
        #region Constructors

        public PageLogPreviewModel()
        {
            
        }

        public PageLogPreviewModel(Page page)
        {
            PageId = page.Id;
            Title = page.Title;
            Caption = page.Caption;
            CaptionWorking = page.CaptionWorking;
            Content = page.Content;
            ContentWorking = page.ContentWorking;

            var pageTags = page.PageTags.Select(t => t.Id);
            var tags = pageTags as int[] ?? pageTags.ToArray();
            Tags = tags.Any() ? string.Join(",", tags.ToList()) : string.Empty;

            FileTemplateId = page.FileTemplateId;
            FileTemplateName = page.FileTemplateId.HasValue ? page.FileTemplate.Name : string.Empty;

            Keywords = page.Keywords;
            IncludeInSiteNavigation = page.IncludeInSiteNavigation;
            StartPublishingDate = page.StartPublishingDate;
            EndPublishingDate = page.EndPublishingDate;
            PageTemplateId = page.PageTemplateId;
            PageTemplateName = page.PageTemplateId.HasValue ? page.PageTemplate.Name : string.Empty;
            Status = page.Status;
            FriendlyUrl = page.FriendlyUrl;
            ParentId = page.ParentId;
            ParentName = page.ParentId.HasValue ? page.Page1.Title : string.Empty;
        }
        #endregion

        #region Public Properties
        public int Id { get; set; }

        public int PageId { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string CaptionWorking { get; set; }

        public string Content { get; set; }

        public string ContentWorking { get; set; }

        public string Tags { get; set; }

        public string Keywords { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }

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
    }
}
