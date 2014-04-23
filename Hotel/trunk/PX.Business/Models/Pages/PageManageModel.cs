using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Services.FileTemplates;
using PX.Business.Services.PageTemplates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Pages;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.Pages
{
    public class PageManageModel : BaseModel, IValidatableObject
    {
        private readonly IPageServices _pageServices;

        public PageManageModel()
        {
            _pageServices = HostContainer.GetInstance<IPageServices>();
            var pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            var fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();

            int position;
            int relativePageId;
            var relativePages = _pageServices.GetRelativePages(out position, out relativePageId);
            StatusList = _pageServices.GetStatus();
            Parents = _pageServices.GetPossibleParents();
            PageTemplates = pageTemplateServices.GetPageTemplateSelectList();
            FileTemplates = fileTemplateServices.GetFileTemplateSelectList();
            Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>();
            TagList = _pageServices.GetPageTags();
            Position = position;
            RelativePageId = relativePageId;
            RelativePages = relativePages;
            IncludeInSiteNavigation = true;
        }

        public PageManageModel(Page page)
        {
            _pageServices = HostContainer.GetInstance<IPageServices>();
            var pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            var fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();
            int position;
            int relativePageId;
            var relativePages = _pageServices.GetRelativePages(out position, out relativePageId, page.Id, page.ParentId);

            Id = page.Id;
            Content = page.Content;
            Title = page.Title;
            FriendlyUrl = page.FriendlyUrl;
            Caption = page.Caption;
            Status = page.Status;
            StatusList = _pageServices.GetStatus();
            ParentId = page.ParentId;
            Parents = _pageServices.GetPossibleParents(page.Id);
            FileTemplateId = page.FileTemplateId;
            FileTemplates = fileTemplateServices.GetFileTemplateSelectList(page.FileTemplateId);
            PageTemplateId = page.PageTemplateId;
            PageTemplates = pageTemplateServices.GetPageTemplateSelectList(page.PageTemplateId);
            Position = position;
            Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>();
            RelativePageId = relativePageId;
            RelativePages = relativePages;
            IncludeInSiteNavigation = page.IncludeInSiteNavigation;
            Tags = page.PageTags.Select(t => t.TagId).ToList();
            TagList = _pageServices.GetPageTags(page.Id);
            StartPublishingDate = page.StartPublishingDate;
            EndPublishingDate = page.EndPublishingDate;
            RecordOrder = page.RecordOrder;
            RecordActive = page.RecordActive;
        }

        public PageManageModel(PageLog log) : this(log.Page)
        {
            Content = log.Content;
            Title = log.Title;
            FriendlyUrl = log.FriendlyUrl;
            Caption = log.Caption;
            Status = log.Status;
            FileTemplateId = log.FileTemplateId;
            PageTemplateId = log.PageTemplateId;
            IncludeInSiteNavigation = log.IncludeInSiteNavigation;
            StartPublishingDate = log.StartPublishingDate;
            EndPublishingDate = log.EndPublishingDate;
        }

        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string FriendlyUrl { get; set; }

        public string Caption { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IncludeInSiteNavigation { get; set; }

        public DateTime? StartPublishingDate { get; set; }

        public DateTime? EndPublishingDate { get; set; }

        public List<int> Tags { get; set; }

        public IEnumerable<SelectListItem> TagList { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<SelectListItem> Parents { get; set; }

        public int? FileTemplateId { get; set; }

        public IEnumerable<SelectListItem> FileTemplates { get; set; }

        public int? PageTemplateId { get; set; }

        public IEnumerable<SelectListItem> PageTemplates { get; set; }

        public int Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        public int Position { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        public int? RelativePageId { get; set; }

        public IEnumerable<SelectListItem> RelativePages { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (_pageServices.IsTitleExisted(Id, Title))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessages:::ExistingTitle:::Title is existed."), new[] { "Title" });
            }

            FriendlyUrl = string.IsNullOrWhiteSpace(FriendlyUrl) ? Title.ToUrlString() : FriendlyUrl.ToUrlString();
            if (_pageServices.IsFriendlyUrlExisted(Id, FriendlyUrl))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessages:::ExistingFriendlyUrl:::Friendly Url is existed."), new[] { "FriendlyUrl" });
            }

            /*Can only choose 1 type of template*/
            if (PageTemplateId.HasValue && FileTemplateId.HasValue)
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessages:::MultipleTemplate:::You can only choose 1 type of template."), new[] { "PageTemplateId", "FileTemplateId" });
            }
        }
        #endregion
    }
}
