using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using AutoMapper;
using PX.Business.Models.News;
using PX.Business.Models.PageAudits;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.PageAudits
{
    public class PageAuditServices : IPageAuditServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public PageAuditServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<PageAudit> GetAll()
        {
            return PageAuditRepository.GetAll();
        }
        public IQueryable<PageAudit> Fetch(Expression<Func<PageAudit, bool>> expression)
        {
            return PageAuditRepository.Fetch(expression);
        }
        public PageAudit FetchFirst(Expression<Func<PageAudit, bool>> expression)
        {
            return PageAuditRepository.FetchFirst(expression);
        }
        public PageAudit GetById(object id)
        {
            return PageAuditRepository.GetById(id);
        }
        public ResponseModel Insert(PageAudit pageAudit)
        {
            return PageAuditRepository.Insert(pageAudit);
        }
        public ResponseModel Update(PageAudit pageAudit)
        {
            return PageAuditRepository.Update(pageAudit);
        }
        public ResponseModel Delete(PageAudit pageAudit)
        {
            return PageAuditRepository.Delete(pageAudit);
        }
        public ResponseModel Delete(object id)
        {
            return PageAuditRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchPageAudits(JqSearchIn si)
        {
            var pageAudits = GetAll().Select(audit => new PageAuditModel
                {
                    Id = audit.Id,
                    PageId = audit.Id,
                    Title = audit.Title,
                    FileTemplateId = audit.FileTemplateId,
                    FileTemplateName = audit.Page.FileTemplateId.HasValue ? audit.Page.FileTemplate.Name : string.Empty,
                    PageTemplateId = audit.PageTemplateId,
                    PageTemplateName = audit.PageTemplateId.HasValue ? audit.Page.PageTemplate.Name : string.Empty,
                    Status = audit.Status,
                    FriendlyUrl = audit.FriendlyUrl,
                    ParentId = audit.ParentId,
                    ParentName = audit.ParentId.HasValue ? audit.Page.Page1.Title : string.Empty,
                    Created = audit.Created,
                    CreatedBy = audit.CreatedBy,
                    Updated = audit.Updated,
                    UpdatedBy = audit.UpdatedBy
                });

            return si.Search(pageAudits);
        }

        #endregion

        /// <summary>
        /// Save current page to audit
        /// </summary>
        /// <param name="pageAuditModel"></param>
        /// <returns></returns>
        public ResponseModel SaveAuditPage(PageAuditViewModel pageAuditModel)
        {
            var page = PageRepository.GetById(pageAuditModel.PageId);
            var sessionId = HttpContext.Current.Session.SessionID;
            if (page != null && sessionId != null)
            {
                var pageAudit = GetAll().FirstOrDefault(a => a.PageId == page.Id && a.SessionId.Equals(sessionId));
                if (pageAudit != null)
                {
                    var changeLog = ChangeLog(pageAudit, pageAuditModel);
                    if (!string.IsNullOrEmpty(changeLog))
                    {
                        pageAudit.ChangeLog += changeLog;
                        return Update(pageAudit);
                    }
                    return new ResponseModel
                        {
                            Success = true
                        };
                }
                Mapper.CreateMap<PageAuditViewModel, PageAudit>();
                var audit = Mapper.Map<PageAuditViewModel, PageAudit>(pageAuditModel);

                pageAudit = GetAll().Where(a => a.PageId == page.Id).OrderByDescending(a => a.Id).FirstOrDefault();
                audit.ChangeLog = pageAudit != null
                                      ? ChangeLog(pageAudit, pageAuditModel)
                                      : string.Format("Created Time: {0}", DateTime.Now);
                if(string.IsNullOrEmpty(audit.ChangeLog))
                {
                    return new ResponseModel
                    {
                        Success = true
                    };
                }
                audit.SessionId = sessionId;
                return Insert(audit);
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
        /// <param name="pageAudit"></param>
        /// <param name="pageAuditModel"></param>
        /// <returns></returns>
        private string ChangeLog(PageAudit pageAudit, PageAuditViewModel pageAuditModel)
        {
            var changeLog = new StringBuilder();
            const string format = "- Update field: {0}\n";
            if (!pageAudit.Title.Equals(pageAuditModel.Title))
            {
                changeLog.AppendFormat(format, "Title");
                pageAudit.Title = pageAudit.Title;
            }
            if (!pageAudit.FriendlyUrl.Equals(pageAuditModel.FriendlyUrl))
            {
                changeLog.AppendFormat(format, "FriendlyUrl");
                pageAudit.FriendlyUrl = pageAudit.FriendlyUrl;
            }
            if (!pageAudit.Content.Equals(pageAuditModel.Content))
            {
                changeLog.AppendFormat(format, "Content");
                pageAudit.Content = pageAudit.Content;
            }
            if (!pageAudit.ContentWorking.Equals(pageAuditModel.ContentWorking))
            {
                changeLog.AppendFormat(format, "ContentWorking");
                pageAudit.ContentWorking = pageAudit.ContentWorking;
            }
            if (!pageAudit.Caption.Equals(pageAuditModel.Caption))
            {
                changeLog.AppendFormat(format, "Caption");
                pageAudit.Caption = pageAudit.Caption;
            }
            if (!pageAudit.CaptionWorking.Equals(pageAuditModel.CaptionWorking))
            {
                changeLog.AppendFormat(format, "CaptionWorking");
                pageAudit.CaptionWorking = pageAudit.CaptionWorking;
            }
            if (!pageAudit.Status.Equals(pageAuditModel.Status))
            {
                changeLog.AppendFormat(format, "Status");
                pageAudit.Status = pageAudit.Status;
            }
            if (!pageAudit.Keywords.Equals(pageAuditModel.Keywords))
            {
                changeLog.AppendFormat(format, "Keywords");
                pageAudit.Keywords = pageAudit.Keywords;
            }
            if (!pageAudit.FileTemplateId.Equals(pageAuditModel.FileTemplateId))
            {
                changeLog.AppendFormat(format, "FileTemplateId");
                pageAudit.FileTemplateId = pageAudit.FileTemplateId;
            }
            if (!pageAudit.PageTemplateId.Equals(pageAuditModel.PageTemplateId))
            {
                changeLog.AppendFormat(format, "PageTemplateId");
                pageAudit.PageTemplateId = pageAudit.PageTemplateId;
            }
            if (!pageAudit.ParentId.Equals(pageAuditModel.ParentId))
            {
                changeLog.AppendFormat(format, "ParentId");
                pageAudit.ParentId = pageAudit.ParentId;
            }
            if (!pageAudit.IncludeInSiteNavigation.Equals(pageAuditModel.IncludeInSiteNavigation))
            {
                changeLog.AppendFormat(format, "IncludeInSiteNavigation");
                pageAudit.IncludeInSiteNavigation = pageAudit.IncludeInSiteNavigation;
            }
            if (!pageAudit.StartPublishingDate.Equals(pageAuditModel.StartPublishingDate))
            {
                changeLog.AppendFormat(format, "StartPublishingDate");
                pageAudit.StartPublishingDate = pageAudit.StartPublishingDate;
            }
            if (!pageAudit.EndPublishingDate.Equals(pageAuditModel.EndPublishingDate))
            {
                changeLog.AppendFormat(format, "EndPublishingDate");
                pageAudit.EndPublishingDate = pageAudit.EndPublishingDate;
            }

            if (!string.IsNullOrEmpty(changeLog.ToString()))
            {
                changeLog.Insert(0,
                                 string.Format("\nUpdate Time: {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            }

            return changeLog.ToString();
        }
    }
}
