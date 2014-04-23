using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.PageTemplateLogs;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.PageTemplateLogs
{
    public class PageTemplateLogServices : IPageTemplateLogServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public PageTemplateLogServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<PageTemplateLog> GetAll()
        {
            return PageTemplateLogRepository.GetAll();
        }
        public IQueryable<PageTemplateLog> Fetch(Expression<Func<PageTemplateLog, bool>> expression)
        {
            return PageTemplateLogRepository.Fetch(expression);
        }
        public PageTemplateLog FetchFirst(Expression<Func<PageTemplateLog, bool>> expression)
        {
            return PageTemplateLogRepository.FetchFirst(expression);
        }
        public PageTemplateLog GetById(object id)
        {
            return PageTemplateLogRepository.GetById(id);
        }
        public ResponseModel Insert(PageTemplateLog pageTemplateLog)
        {
            return PageTemplateLogRepository.Insert(pageTemplateLog);
        }
        public ResponseModel Update(PageTemplateLog pageTemplateLog)
        {
            return PageTemplateLogRepository.Update(pageTemplateLog);
        }
        public ResponseModel Delete(PageTemplateLog pageTemplateLog)
        {
            return PageTemplateLogRepository.Delete(pageTemplateLog);
        }
        public ResponseModel Delete(object id)
        {
            return PageTemplateLogRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return PageTemplateLogRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the PageTemplateLogs.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchPageTemplateLogs(JqSearchIn si)
        {
            var pageTemplateLogs = GetAll().Select(u => new PageTemplateLogModel
            {
                Id = u.Id,
                Name = u.Name,
                ParentId = u.ParentId,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(pageTemplateLogs);
        }
        
        #endregion
    }
}
