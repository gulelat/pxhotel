using System;
using System.Collections.Generic;
using PX.EntityModel;

namespace PX.Business.Models.Pages.Logs
{
    public class PageLogsModel
    {
        public List<PageLogItem> Logs { get; set; }

        public string SessionId { get; set; }

        public User Creator { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int Total { get; set; }
    }
}
