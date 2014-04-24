﻿using System.Collections.Generic;
using PX.Business.Models.PageTemplateLogs;

namespace PX.Business.Models.PageTemplates
{
    public class PageTemplateLogsModel
    {
        #region Constructors
        public PageTemplateLogsModel()
        {

        }
        #endregion

        #region Public Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public bool LoadComplete { get; set; }

        public List<PageTemplateLogViewModel> Logs { get; set; }

        #endregion
    }
}
