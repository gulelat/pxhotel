using System.Collections.Generic;
using PX.Business.Models.Pages.Logs;

namespace PX.Business.Models.Pages
{
    public class PageLogListingModel
    {
        #region Constructors
        public PageLogListingModel()
        {
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int Total { get; set; }

        public bool LoadComplete { get; set; }

        public List<PageLogsModel> Logs { get; set; }

        #endregion
    }
}
