using System;
using PX.EntityModel;

namespace PX.Business.Models.Templates.Logs
{
    public class TemplateLogItem
    {
        #region Constructor
        public TemplateLogItem()
        {
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
}