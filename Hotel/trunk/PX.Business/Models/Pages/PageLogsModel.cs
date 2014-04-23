using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.PageLogs;
using PX.EntityModel;

namespace PX.Business.Models.Pages
{
    public class PageLogsModel
    {
        #region Constructors
        public PageLogsModel()
        {

        }

        public PageLogsModel(Page page)
        {
            Id = page.Id;
            Title = page.Title;
            Url = page.FriendlyUrl;
            Logs = page.PageLogs.OrderByDescending(l => l.Created)
                .Select(l => new PageLogViewModel(l)).ToList();
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public List<PageLogViewModel> Logs { get; set; }

        #endregion
    }
}
