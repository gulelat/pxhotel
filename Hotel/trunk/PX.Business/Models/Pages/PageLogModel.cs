using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.PageAudits;
using PX.EntityModel;

namespace PX.Business.Models.Pages
{
    public class PageLogModel
    {
        #region Constructors
        public PageLogModel()
        {

        }

        public PageLogModel(Page page)
        {
            Id = page.Id;
            Title = page.Title;
            Url = page.FriendlyUrl;
            Logs = page.PageAudits.OrderByDescending(l => l.Created)
                .Select(l => new PageAuditModel(l));
        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public List<PageAuditModel> Logs { get; set; }

        #endregion
    }
}
