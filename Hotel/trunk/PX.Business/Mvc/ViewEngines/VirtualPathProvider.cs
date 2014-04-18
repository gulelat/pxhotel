using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using PX.Business.Models.Pages;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Pages;
using PX.Core.Framework.Mvc.Environments;
using PX.EntityModel;
using RazorEngine.Templating;

namespace PX.Business.Mvc.ViewEngines
{
    public class MyVirtualPathProvider : VirtualPathProvider
    {
        private readonly IPageTemplateServices _pageTemplateServices;
        public MyVirtualPathProvider()
        {
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
        }

        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return base.CombineVirtualPaths(basePath, relativePath);
        }

        public override string GetFileHash(string virtualPath, System.Collections.IEnumerable virtualPathDependencies)
        {
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override bool FileExists(string virtualPath)
        {
            if (base.FileExists(virtualPath))
            {
                return true;
            }
            return _pageTemplateServices.IsPageTemplateExisted(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (base.FileExists(virtualPath))
            {
                return base.GetFile(virtualPath);
            }
            var template = FindTemplate(virtualPath);
            if (template != null)
                return new MyVirtualFile(virtualPath, template);
            return null;
        }

        private PageTemplate FindTemplate(string virtualPath)
        {
            return _pageTemplateServices.FindTemplate(virtualPath);
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

            public MyVirtualFile(string virtualPath, PageTemplate template)
                : base(virtualPath)
            {
                var pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
                var content = pageTemplateServices.RenderPageTemplate(template.Id);
                _data = System.Text.Encoding.ASCII.GetBytes(content);
            }

            public override Stream Open()
            {
                return new MemoryStream(_data);
            }
        }
    }
}