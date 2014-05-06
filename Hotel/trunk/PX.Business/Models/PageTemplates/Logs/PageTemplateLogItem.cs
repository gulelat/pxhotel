using System;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Environments;
using PX.EntityModel;

namespace PX.Business.Models.PageTemplates.Logs
{
    public class PageTemplateLogItem
    {
        #region Constructor
        private readonly IUserServices _userServices;
        public PageTemplateLogItem()
        {
            _userServices = HostContainer.GetInstance<IUserServices>();
        }


        public PageTemplateLogItem(PageTemplateLog model): this()
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
