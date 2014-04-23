using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using AutoMapper;
using PX.Business.Models.PageAudits;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
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
                    ChangeLog = audit.ChangeLog,
                    FileTemplateId = audit.FileTemplateId,
                    FileTemplateName = audit.Page.FileTemplateId.HasValue ? audit.Page.FileTemplate.Name : string.Empty,
                    PageTemplateId = audit.PageTemplateId,
                    PageTemplateName = audit.PageTemplateId.HasValue ? audit.Page.PageTemplate.Name : string.Empty,
                    Status = audit.Status,
                    FriendlyUrl = audit.FriendlyUrl,
                    ParentId = audit.ParentId,
                    ParentName = audit.ParentId.HasValue ? audit.Page.Page1.Title : string.Empty,
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
            if (page != null)
            {
                /*
                 * Map page audit model to audit entity
                 * Get last updated version of audit page
                 * Create Change Log
                 * If there are nothing change then do not do anything
                 * Otherwise insert audit
                 */
                Mapper.CreateMap<PageAuditViewModel, PageAudit>();
                var audit = Mapper.Map<PageAuditViewModel, PageAudit>(pageAuditModel);

                var pageAudit = GetAll().Where(a => a.PageId == page.Id).OrderByDescending(a => a.Id).FirstOrDefault();

                audit.ChangeLog = pageAudit != null
                                      ? ChangeLog(pageAudit, pageAuditModel)
                                      : string.Format("Created: {0}", DateTime.Now);

                if (string.IsNullOrEmpty(audit.ChangeLog))
                {
                    return new ResponseModel
                    {
                        Success = true
                    };
                }
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
            if (!ConvertUtilities.Compare(pageAudit.Title, pageAuditModel.Title))
            {
                changeLog.AppendFormat(format, "Title");
                pageAudit.Title = pageAuditModel.Title;
            }
            if (!ConvertUtilities.Compare(pageAudit.FriendlyUrl, pageAuditModel.FriendlyUrl))
            {
                changeLog.AppendFormat(format, "FriendlyUrl");
                pageAudit.FriendlyUrl = pageAuditModel.FriendlyUrl;
            }
            if (!ConvertUtilities.Compare(pageAudit.Content, pageAuditModel.Content))
            {
                changeLog.AppendFormat(format, "Content");
                pageAudit.Content = pageAuditModel.Content;
            }
            if (!ConvertUtilities.Compare(pageAudit.ContentWorking, pageAuditModel.ContentWorking))
            {
                changeLog.AppendFormat(format, "ContentWorking");
                pageAudit.ContentWorking = pageAuditModel.ContentWorking;
            }
            if (!ConvertUtilities.Compare(pageAudit.Caption, pageAuditModel.Caption))
            {
                changeLog.AppendFormat(format, "Caption");
                pageAudit.Caption = pageAuditModel.Caption;
            }
            if (!ConvertUtilities.Compare(pageAudit.CaptionWorking, pageAuditModel.CaptionWorking))
            {
                changeLog.AppendFormat(format, "CaptionWorking");
                pageAudit.CaptionWorking = pageAuditModel.CaptionWorking;
            }
            if (!ConvertUtilities.Compare(pageAudit.Status, pageAuditModel.Status))
            {
                changeLog.AppendFormat(format, "Status");
                pageAudit.Status = pageAuditModel.Status;
            }
            if (!ConvertUtilities.Compare(pageAudit.Keywords, pageAuditModel.Keywords))
            {
                changeLog.AppendFormat(format, "Keywords");
                pageAudit.Keywords = pageAuditModel.Keywords;
            }
            if (!ConvertUtilities.Compare(pageAudit.FileTemplateId, pageAuditModel.FileTemplateId))
            {
                changeLog.AppendFormat(format, "FileTemplateId");
                pageAudit.FileTemplateId = pageAuditModel.FileTemplateId;
            }
            if (!ConvertUtilities.Compare(pageAudit.PageTemplateId, pageAuditModel.PageTemplateId))
            {
                changeLog.AppendFormat(format, "PageTemplateId");
                pageAudit.PageTemplateId = pageAuditModel.PageTemplateId;
            }
            if (!ConvertUtilities.Compare(pageAudit.ParentId, pageAuditModel.ParentId))
            {
                changeLog.AppendFormat(format, "ParentId");
                pageAudit.ParentId = pageAuditModel.ParentId;
            }
            if (!ConvertUtilities.Compare(pageAudit.IncludeInSiteNavigation, pageAuditModel.IncludeInSiteNavigation))
            {
                changeLog.AppendFormat(format, "IncludeInSiteNavigation");
                pageAudit.IncludeInSiteNavigation = pageAuditModel.IncludeInSiteNavigation;
            }
            if (!ConvertUtilities.Compare(pageAudit.StartPublishingDate, pageAuditModel.StartPublishingDate))
            {
                changeLog.AppendFormat(format, "StartPublishingDate");
                pageAudit.StartPublishingDate = pageAuditModel.StartPublishingDate;
            }
            if (!ConvertUtilities.Compare(pageAudit.EndPublishingDate, pageAuditModel.EndPublishingDate))
            {
                changeLog.AppendFormat(format, "EndPublishingDate");
                pageAudit.EndPublishingDate = pageAuditModel.EndPublishingDate;
            }

            if (!string.IsNullOrEmpty(changeLog.ToString()))
            {
                changeLog.Insert(0,
                                 string.Format("** Update: {0} **\n", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            }

            return changeLog.ToString();
        }
    }
}
