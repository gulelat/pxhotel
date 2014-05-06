using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using PX.Business.Models.TemplateLogs;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using AutoMapper;

namespace PX.Business.Services.TemplateLogs
{
    public class TemplateLogServices : ITemplateLogServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly TemplateLogRepository _templateLogRepository;
        private readonly TemplateRepository _templateRepository;
        public TemplateLogServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _templateLogRepository = new TemplateLogRepository(entities);
            _templateRepository = new TemplateRepository(entities);
        }

        #region Base
        public IQueryable<TemplateLog> GetAll()
        {
            return _templateLogRepository.GetAll();
        }
        public IQueryable<TemplateLog> Fetch(Expression<Func<TemplateLog, bool>> expression)
        {
            return _templateLogRepository.Fetch(expression);
        }
        public TemplateLog FetchFirst(Expression<Func<TemplateLog, bool>> expression)
        {
            return _templateLogRepository.FetchFirst(expression);
        }
        public TemplateLog GetById(object id)
        {
            return _templateLogRepository.GetById(id);
        }
        public ResponseModel Insert(TemplateLog templateLog)
        {
            return _templateLogRepository.Insert(templateLog);
        }
        public ResponseModel Update(TemplateLog templateLog)
        {
            return _templateLogRepository.Update(templateLog);
        }
        public ResponseModel Delete(TemplateLog templateLog)
        {
            return _templateLogRepository.Delete(templateLog);
        }
        public ResponseModel Delete(object id)
        {
            return _templateLogRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _templateLogRepository.InactiveRecord(id);
        }
        #endregion

        #region Logs
        /// <summary>
        /// Save current page to audit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveTemplateLog(TemplateLogManageModel model)
        {
            var template = _templateRepository.GetById(model.TemplateId);
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
                log.SessionId = HttpContext.Current.Session.SessionID;
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
                changeLog.Insert(0, string.Format("** Update Template **\n"));
            }

            return changeLog.ToString();
        }

        #endregion
    }
}
