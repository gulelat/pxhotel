using System;
using System.Collections.Generic;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Environments;
using PX.EntityModel;

namespace PX.Business.Models.TemplateLogs
{
    public class TemplateLogItem
    {
        #region Constructor
        private readonly IUserServices _userServices;
        public TemplateLogItem()
        {
            _userServices = HostContainer.GetInstance<IUserServices>();
        }


        public TemplateLogItem(TemplateLog model)
            : this()
        {
            Id = model.Id;
            Name = model.Name;
            SessionId = model.SessionId;
            ChangeLog = model.ChangeLog;
            Created = model.Created;
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string SessionId { get; set; }

        public string Name { get; set; }

        public string ChangeLog { get; set; }

        public DateTime Created { get; set; }

        #endregion
    }

    public class TemplateLogsViewModel
    {
        public List<TemplateLogItem> Logs { get; set; }

        public string SessionId { get; set; }

        public User Creator { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int Total { get; set; }
    }
}
