﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PX.Business.Models.TemplateLogs;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using AutoMapper;

namespace PX.Business.Services.TemplateLogs
{
    public class TemplateLogServices : ITemplateLogServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public TemplateLogServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<TemplateLog> GetAll()
        {
            return TemplateLogRepository.GetAll();
        }
        public IQueryable<TemplateLog> Fetch(Expression<Func<TemplateLog, bool>> expression)
        {
            return TemplateLogRepository.Fetch(expression);
        }
        public TemplateLog FetchFirst(Expression<Func<TemplateLog, bool>> expression)
        {
            return TemplateLogRepository.FetchFirst(expression);
        }
        public TemplateLog GetById(object id)
        {
            return TemplateLogRepository.GetById(id);
        }
        public ResponseModel Insert(TemplateLog templateLog)
        {
            return TemplateLogRepository.Insert(templateLog);
        }
        public ResponseModel Update(TemplateLog templateLog)
        {
            return TemplateLogRepository.Update(templateLog);
        }
        public ResponseModel Delete(TemplateLog templateLog)
        {
            return TemplateLogRepository.Delete(templateLog);
        }
        public ResponseModel Delete(object id)
        {
            return TemplateLogRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return TemplateLogRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the TemplateLogs.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchTemplateLogs(JqSearchIn si)
        {
            var templateLogs = GetAll().Select(u => new TemplateLogModel
            {
                Id = u.Id,
                TemplateId = u.TemplateId,
                Name = u.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(templateLogs);
        }

        #endregion

        /// <summary>
        /// Save current page to audit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveTemplateLog(TemplateLogManageModel model)
        {
            var template = TemplateRepository.GetById(model.TemplateId);
            if (template != null)
            {
                /*
                 * Map template log model to log entity
                 * Get last updated version of template log
                 * If there are nothing change then do not do anything
                 * Otherwise insert log
                 */
                Mapper.CreateMap<TemplateLogManageModel, TemplateLog>();
                var log = Mapper.Map<TemplateLogManageModel, TemplateLog>(model);

                var templateLog = GetAll().Where(a => a.TemplateId == template.Id).OrderByDescending(a => a.Id).FirstOrDefault();

                log.ChangeLog = templateLog != null
                                      ? ChangeLog(templateLog, model)
                                      : string.Format("** Create Template **");

                if (string.IsNullOrEmpty(log.ChangeLog))
                {
                    return new ResponseModel
                    {
                        Success = true
                    };
                }
                return Insert(log);
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Templates:::Messages:::ObjectNotFounded:::Template is not founded.")
            };
        }

        /// <summary>
        /// Update data and create change log
        /// </summary>
        /// <param name="templateLog"></param>
        /// <param name="templateLogModel"></param>
        /// <returns></returns>
        private string ChangeLog(TemplateLog templateLog, TemplateLogManageModel templateLogModel)
        {
            var changeLog = new StringBuilder();
            const string format = "- Update field: {0}\n";
            if (!ConvertUtilities.Compare(templateLog.Name, templateLogModel.Name))
            {
                changeLog.AppendFormat(format, "Name");
                templateLog.Name = templateLogModel.Name;
            }
            if (!ConvertUtilities.Compare(templateLog.Content, templateLogModel.Content))
            {
                changeLog.AppendFormat(format, "Content");
                templateLog.Content = templateLogModel.Content;
            }

            if (!string.IsNullOrEmpty(changeLog.ToString()))
            {
                changeLog.Insert(0, string.Format("** Update Page Template **\n"));
            }

            return changeLog.ToString();
        }
    }
}
