using System.Collections.Generic;
using PX.Business.Models.TemplateLogs;

namespace PX.Business.Models.Templates
{
    public class TemplateLogsModel
    {
        #region Constructors
        public TemplateLogsModel()
        {

        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public bool LoadComplete { get; set; }

        public List<TemplateLogViewModel> Logs { get; set; }

        #endregion
    }
}
