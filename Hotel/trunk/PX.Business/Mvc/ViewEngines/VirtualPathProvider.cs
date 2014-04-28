﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using PX.Business.Models.Pages;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Pages;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Mvc.ViewEngines
{
    public class MyVirtualPathProvider : VirtualPathProvider
    {
        private const string DBTemplate = "DBTemplate";
        private IPageTemplateServices _pageTemplateServices;

        #region Private Methods
        /// <summary>
        /// Check if virtual path is Db template or not
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        private static bool IsPathIsDbTemplate(string virtualPath)
        {
            var templates = virtualPath.Split('/').Last().Split('.');
            if (!templates.First().Equals(DBTemplate, StringComparison.InvariantCultureIgnoreCase) || templates.Count() < 3)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get file template from virtual path
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        private PageTemplate FindTemplate(string virtualPath)
        {
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            return _pageTemplateServices.FindTemplate(virtualPath);
        }

        #endregion

        /// <summary>
        /// Check if template is modified to re-cache
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="virtualPathDependencies"></param>
        /// <returns></returns>
        public override string GetFileHash(string virtualPath, System.Collections.IEnumerable virtualPathDependencies)
        {
            if(IsPathIsDbTemplate(virtualPath))
            {
                var template = FindTemplate(virtualPath);
                return TemplateServices.GetTemplateCacheName(template.Name, template.Created, template.Updated);
            }
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        /// <summary>
        /// Return cache version of razor file
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="virtualPathDependencies"></param>
        /// <param name="utcStart"></param>
        /// <returns></returns>
        public override CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (base.FileExists(virtualPath))
            {
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }
            return null;
        }

        /// <summary>
        /// Check if file exist in physical path or not, if not search in db template
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override bool FileExists(string virtualPath)
        {
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            if (IsPathIsDbTemplate(virtualPath))
            {
                return _pageTemplateServices.IsPageTemplateExisted(virtualPath);
            }
            return base.FileExists(virtualPath);
        }

        /// <summary>
        /// Get file by virtual path
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsPathIsDbTemplate(virtualPath))
            {
                var template = FindTemplate(virtualPath);
                if (template != null)
                    return new MyVirtualFile(virtualPath, template);
            }
            return base.GetFile(virtualPath);
        }
    }

    public class MyVirtualFile : VirtualFile
    {
        private readonly byte[] _data;

        internal byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Generate virtual file from template
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="template"></param>
        public MyVirtualFile(string virtualPath, PageTemplate template)
            : base(virtualPath)
        {
            var pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            var pageServices = HostContainer.GetInstance<IPageServices>();
            var curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();

            var pageId = HttpContext.Current.Request["activePageId"].ToNullableInt();
            var page = pageServices.GetById(pageId);
            var model = page != null ? new PageRenderModel(page) : new PageRenderModel();
            var content = pageTemplateServices.RenderPageTemplate(template.Id, model);
            content = curlyBracketServices.Render(content);
            _data = Encoding.UTF8.GetBytes(content);
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(content);
            _data = Encoding.Convert(utf8, iso, utfBytes);
        }

        public override Stream Open()
        {
            return new MemoryStream(_data);
        }
    }
}