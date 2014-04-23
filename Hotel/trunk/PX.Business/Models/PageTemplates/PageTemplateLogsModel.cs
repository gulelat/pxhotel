using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.PageLogs;
using PX.Business.Models.PageTemplateLogs;
using PX.EntityModel;

namespace PX.Business.Models.PageTemplates
{
    public class PageTemplateLogsModel
    {
        #region Constructors
        public PageTemplateLogsModel()
        {

        }

        public PageTemplateLogsModel(PageTemplate pageTemplate)
        {
            Id = pageTemplate.Id;
            Name = pageTemplate.Name;
            Logs = pageTemplate.PageTemplateLogs.OrderByDescending(l => l.Created)
                .Select(l => new PageTemplateLogViewModel(l)).ToList();
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public List<PageTemplateLogViewModel> Logs { get; set; }

        #endregion
    }
}
