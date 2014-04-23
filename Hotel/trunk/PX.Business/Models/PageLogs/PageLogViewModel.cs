using System;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.PageLogs
{
    public class PageLogViewModel
    {
        #region Constructors

        private readonly IUserServices _userServices;
        public PageLogViewModel()
        {
            _userServices = HostContainer.GetInstance<IUserServices>();
        }

        public PageLogViewModel(PageLog pageLog): this()
        {
            Id = pageLog.Id;
            PageId = pageLog.PageId;
            Title = pageLog.Title;
            ChangeLog = pageLog.ChangeLog;
            FileTemplateId = pageLog.FileTemplateId;
            PageTemplateId = pageLog.Page.PageTemplateId;
            Status = pageLog.Status;
            FriendlyUrl = pageLog.FriendlyUrl;
            ParentId = pageLog.ParentId;
            Created = pageLog.Created;
            Creator = _userServices.GetUser(pageLog.CreatedBy);
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

        public User Creator { get; set; }

        #endregion
    }
}
