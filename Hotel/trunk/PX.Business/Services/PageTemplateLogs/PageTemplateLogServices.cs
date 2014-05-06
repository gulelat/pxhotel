﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using PX.Business.Models.PageTemplateLogs;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using AutoMapper;

namespace PX.Business.Services.PageTemplateLogs
{
    public class PageTemplateLogServices : IPageTemplateLogServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly PageTemplateLogRepository _pageTemplateLogRepository;
        private readonly PageTemplateRepository _pageTemplateRepository;
        public PageTemplateLogServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _pageTemplateLogRepository = new PageTemplateLogRepository(entities);
            _pageTemplateRepository = new PageTemplateRepository(entities);
        }

        #region Base
        public IQueryable<PageTemplateLog> GetAll()
        {
            return _pageTemplateLogRepository.GetAll();
        }
        public IQueryable<PageTemplateLog> Fetch(Expression<Func<PageTemplateLog, bool>> expression)
        {
            return _pageTemplateLogRepository.Fetch(expression);
        }
        public PageTemplateLog FetchFirst(Expression<Func<PageTemplateLog, bool>> expression)
        {
            return _pageTemplateLogRepository.FetchFirst(expression);
        }
        public PageTemplateLog GetById(object id)
        {
            return _pageTemplateLogRepository.GetById(id);
        }
        public ResponseModel Insert(PageTemplateLog pageTemplateLog)
        {
            return _pageTemplateLogRepository.Insert(pageTemplateLog);
        }
        public ResponseModel Update(PageTemplateLog pageTemplateLog)
        {
            return _pageTemplateLogRepository.Update(pageTemplateLog);
        }
        public ResponseModel Delete(PageTemplateLog pageTemplateLog)
        {
            return _pageTemplateLogRepository.Delete(pageTemplateLog);
        }
        public ResponseModel Delete(object id)
        {
            return _pageTemplateLogRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _pageTemplateLogRepository.InactiveRecord(id);
        }
        #endregion

        #region Logs
        /// <summary>
        /// Save current page to audit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SavePageTemplateLog(PageTemplateLogManageModel model)
        {
            var pageTemplate = _pageTemplateRepository.GetById(model.PageTemplateId);
            if (pageTemplate != null)
            {
                /*
                 * Map page template log model to log entity
                 * Get last updated version of template log
                 * If there are nothing change then do not do anything
                 * Otherwise insert log
                 */
                Mapper.CreateMap<PageTemplateLogManageModel, PageTemplateLog>();
                var log = Mapper.Map<PageTemplateLogManageModel, PageTemplateLog>(model);

                var pageTemplateLog = GetAll().Where(a => a.PageTemplateId == pageTemplate.Id).OrderByDescending(a => a.Id).FirstOrDefault();

                log.ChangeLog = pageTemplateLog != null
                                      ? ChangeLog(pageTemplateLog, model)
                                      : string.Format("** Create Page Template **");

                if (string.IsNullOrEmpty(log.ChangeLog))
                {
                    return new ResponseModel
                    {
                        Success = true
                    };
                }
                log.SessionId = HttpContext.Current.Session.SessionID;
                return Insert(log);
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::ObjectNotFounded:::Page Template is not founded.")
            };
        }

        /// <summary>
        /// Update data and create change log
        /// </summary>
        /// <param name="pageTemplateLog"></param>
        /// <param name="pageTemplateLogModel"></param>
        /// <returns></returns>
        private string ChangeLog(PageTemplateLog pageTemplateLog, PageTemplateLogManageModel pageTemplateLogModel)
        {
            var changeLog = new StringBuilder();
            const string format = "- Update field: {0}\n";
            if (!ConvertUtilities.Compare(pageTemplateLog.Name, pageTemplateLogModel.Name))
            {
                changeLog.AppendFormat(format, "Name");
                pageTemplateLog.Name = pageTemplateLogModel.Name;
            }
            if (!ConvertUtilities.Compare(pageTemplateLog.Content, pageTemplateLogModel.Content))
            {
                changeLog.AppendFormat(format, "Content");
                pageTemplateLog.Content = pageTemplateLogModel.Content;
            }
            if (!ConvertUtilities.Compare(pageTemplateLog.ParentId, pageTemplateLogModel.ParentId))
            {
                changeLog.AppendFormat(format, "ParentId");
                pageTemplateLog.ParentId = pageTemplateLogModel.ParentId;
            }

            if (!string.IsNullOrEmpty(changeLog.ToString()))
            {
                changeLog.Insert(0, string.Format("** Update Page Template **\n"));
            }

            return changeLog.ToString();
        }

        #endregion
    }
}
