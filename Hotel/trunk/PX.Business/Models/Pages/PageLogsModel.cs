using System.Collections.Generic;
using PX.Business.Models.PageLogs;

namespace PX.Business.Models.Pages
{
    public class PageLogsModel
    {
        #region Constructors
        public PageLogsModel()
        {
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public bool LoadComplete { get; set; }

        public List<PageLogViewModel> Logs { get; set; }

        #endregion
    }
}
