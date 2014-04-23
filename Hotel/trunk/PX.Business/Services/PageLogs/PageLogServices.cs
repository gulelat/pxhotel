using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using PX.Business.Models.PageLogs;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.PageLogs
{
    public class PageLogServices : IPageLogServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public PageLogServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<PageLog> GetAll()
        {
            return PageLogRepository.GetAll();
        }
        public IQueryable<PageLog> Fetch(Expression<Func<PageLog, bool>> expression)
        {
            return PageLogRepository.Fetch(expression);
        }
        public PageLog FetchFirst(Expression<Func<PageLog, bool>> expression)
        {
            return PageLogRepository.FetchFirst(expression);
        }
        public PageLog GetById(object id)
        {
            return PageLogRepository.GetById(id);
        }
        public ResponseModel Insert(PageLog pageLog)
        {
            return PageLogRepository.Insert(pageLog);
        }
        public ResponseModel Update(PageLog pageLog)
        {
            return PageLogRepository.Update(pageLog);
        }
        public ResponseModel Delete(PageLog pageLog)
        {
            return PageLogRepository.Delete(pageLog);
        }
        public ResponseModel Delete(object id)
        {
            return PageLogRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchPageLogs(JqSearchIn si)
        {
            var pageLogs = GetAll().Select(audit => new PageLogModel
                {
                    Id = audit.Id,
                    PageId = audit.PageId,
                    Title = audit.Title,
                    ChangeLog = audit.ChangeLog,
                    FileTemplateId = audit.FileTemplateId,
                    PageTemplateId = audit.PageTemplateId,
                    Status = audit.Status,
                    FriendlyUrl = audit.FriendlyUrl,
                    ParentId = audit.ParentId,
                    Created = audit.Created,
                    CreatedBy = audit.CreatedBy
                });

            return si.Search(pageLogs);
        }

        #endregion

        /// <summary>
        /// Save current page to audit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SavePageLog(PageLogManageModel model)
        {
            var page = PageRepository.GetById(model.PageId);
            if (page != null)
            {
                /*
                 * Map page log model to log entity
                 * Get last updated version of page log
                 * Create Change Log
                 * If there are nothing change then do not do anything
                 * Otherwise insert log
                 */
                Mapper.CreateMap<PageLogManageModel, PageLog>();
                var log = Mapper.Map<PageLogManageModel, PageLog>(model);

                var pageLog = GetAll().Where(a => a.PageId == page.Id).OrderByDescending(a => a.Id).FirstOrDefault();

                log.ChangeLog = pageLog != null
                                      ? ChangeLog(pageLog, model)
                                      : string.Format("** Create Page **");

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
                    Message = _localizedResourceServices.T("AdminModule:::Pages:::Messages:::ObjectNotFounded:::Page is not founded.")
                };
        }

        /// <summary>
        /// Update data and create change log
        /// </summary>
        /// <param name="pageLog"></param>
        /// <param name="pageLogModel"></param>
        /// <returns></returns>
        private string ChangeLog(PageLog pageLog, PageLogManageModel pageLogModel)
        {
            var changeLog = new StringBuilder();
            const string format = "- Update field: {0}\n";
            if (!ConvertUtilities.Compare(pageLog.Title, pageLogModel.Title))
            {
                changeLog.AppendFormat(format, "Title");
                pageLog.Title = pageLogModel.Title;
            }
            if (!ConvertUtilities.Compare(pageLog.FriendlyUrl, pageLogModel.FriendlyUrl))
            {
                changeLog.AppendFormat(format, "FriendlyUrl");
                pageLog.FriendlyUrl = pageLogModel.FriendlyUrl;
            }
            if (!ConvertUtilities.Compare(pageLog.Content, pageLogModel.Content))
            {
                changeLog.AppendFormat(format, "Content");
                pageLog.Content = pageLogModel.Content;
            }
            if (!ConvertUtilities.Compare(pageLog.ContentWorking, pageLogModel.ContentWorking))
            {
                changeLog.AppendFormat(format, "ContentWorking");
                pageLog.ContentWorking = pageLogModel.ContentWorking;
            }
            if (!ConvertUtilities.Compare(pageLog.Caption, pageLogModel.Caption))
            {
                changeLog.AppendFormat(format, "Caption");
                pageLog.Caption = pageLogModel.Caption;
            }
            if (!ConvertUtilities.Compare(pageLog.CaptionWorking, pageLogModel.CaptionWorking))
            {
                changeLog.AppendFormat(format, "CaptionWorking");
                pageLog.CaptionWorking = pageLogModel.CaptionWorking;
            }
            if (!ConvertUtilities.Compare(pageLog.Status, pageLogModel.Status))
            {
                changeLog.AppendFormat(format, "Status");
                pageLog.Status = pageLogModel.Status;
            }
            if (!ConvertUtilities.Compare(pageLog.Keywords, pageLogModel.Keywords))
            {
                changeLog.AppendFormat(format, "Keywords");
                pageLog.Keywords = pageLogModel.Keywords;
            }
            if (!ConvertUtilities.Compare(pageLog.FileTemplateId, pageLogModel.FileTemplateId))
            {
                changeLog.AppendFormat(format, "FileTemplateId");
                pageLog.FileTemplateId = pageLogModel.FileTemplateId;
            }
            if (!ConvertUtilities.Compare(pageLog.PageTemplateId, pageLogModel.PageTemplateId))
            {
                changeLog.AppendFormat(format, "PageTemplateId");
                pageLog.PageTemplateId = pageLogModel.PageTemplateId;
            }
            if (!ConvertUtilities.Compare(pageLog.ParentId, pageLogModel.ParentId))
            {
                changeLog.AppendFormat(format, "ParentId");
                pageLog.ParentId = pageLogModel.ParentId;
            }
            if (!ConvertUtilities.Compare(pageLog.IncludeInSiteNavigation, pageLogModel.IncludeInSiteNavigation))
            {
                changeLog.AppendFormat(format, "IncludeInSiteNavigation");
                pageLog.IncludeInSiteNavigation = pageLogModel.IncludeInSiteNavigation;
            }
            if (!ConvertUtilities.Compare(pageLog.StartPublishingDate, pageLogModel.StartPublishingDate))
            {
                changeLog.AppendFormat(format, "StartPublishingDate");
                pageLog.StartPublishingDate = pageLogModel.StartPublishingDate;
            }
            if (!ConvertUtilities.Compare(pageLog.EndPublishingDate, pageLogModel.EndPublishingDate))
            {
                changeLog.AppendFormat(format, "EndPublishingDate");
                pageLog.EndPublishingDate = pageLogModel.EndPublishingDate;
            }

            if (!string.IsNullOrEmpty(changeLog.ToString()))
            {
                changeLog.Insert(0, string.Format("** Update Page **\n"));
            }

            return changeLog.ToString();
        }
    }
}
