﻿using System;
using System.Collections.Generic;
using PX.EntityModel;

namespace PX.Business.Models.PageTemplates.Logs
{
    public class PageTemplateLogsModel
    {
        public List<PageTemplateLogItem> Logs { get; set; }

        public string SessionId { get; set; }

        public User Creator { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int Total { get; set; }
    }
}
