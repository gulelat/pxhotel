using System.Collections.Generic;
using PX.Business.Models.Templates.Logs;

namespace PX.Business.Models.Templates
{
    public class TemplateLogListingModel
    {
        #region Constructors
        public TemplateLogListingModel()
        {

        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public int Total { get; set; }

        public bool LoadComplete { get; set; }

        public List<TemplateLogsModel> Logs { get; set; }

        #endregion
    }
}
