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
        private readonly IPageTemplateServices _pageTemplateServices;
        private readonly IFileTemplateServices _fileTemplateServices;
        public PageManageModel()
        {
            _pageServices = HostContainer.GetInstance<IPageServices>();
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            _fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();

            int position;
            int relativePageId;
            var relativePages = _pageServices.GetRelativePages(out position, out relativePageId);
            StatusList = _pageServices.GetStatus();
            Parents = _pageServices.GetPossibleParents();
            PageTemplates = _pageTemplateServices.GetPageTemplateSelectList();
            FileTemplates = _fileTemplateServices.GetFileTemplateSelectList();
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
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            _fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();
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
            FileTemplates = _fileTemplateServices.GetFileTemplateSelectList(page.FileTemplateId);
            PageTemplateId = page.PageTemplateId;
            PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(page.PageTemplateId);
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

        public int? ParentId { get; set; }

        public List<int> Tags { get; set; }

        public IEnumerable<SelectListItem> TagList { get; set; }

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
            var pageServices = HostContainer.GetInstance<IPageServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (pageServices.IsTitleExisted(Id, Title))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessages:::ExistingTitle:::Title is existed."), new[] { "Title" });
            }

            FriendlyUrl = string.IsNullOrWhiteSpace(FriendlyUrl) ? Title.ToUrlString() : FriendlyUrl.ToUrlString();
            if (pageServices.IsFriendlyUrlExisted(Id, FriendlyUrl))
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
