using System;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Environments;
using PX.EntityModel;

namespace PX.Business.Models.PageTemplateLogs
{
    public class PageTemplateLogViewModel
    {
        #region Constructor
        private readonly IUserServices _userServices;
        public PageTemplateLogViewModel()
        {
            _userServices = HostContainer.GetInstance<IUserServices>();
        }


        public PageTemplateLogViewModel(PageTemplateLog model): this()
        {
            Id = model.Id;
            Name = model.Name;
            ChangeLog = model.ChangeLog;
            Created = model.Created;
            Creator = _userServices.GetUser(model.CreatedBy);
            ParentId = model.ParentId;
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string ChangeLog { get; set; }

        public int? ParentId { get; set; }

        public DateTime Created { get; set; }

        public User Creator { get; set; }

        #endregion
    }
}
