using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using PX.Business.Models.Pages;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Pages;
using PX.EntityModel;
using RazorEngine.Templating;

namespace PX.Business.Mvc.ViewEngines.Razor
{
    public class ViewEngine : RazorViewEngine
    {
        public ViewEngine(){
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext,
           string viewName, string masterName, bool useCache)
        {
            if (viewName.StartsWith("ControlGroup"))
            {
                    return new ViewEngineResult(new MyView(new object()), this);
            }
            var viewResult = base.FindView(controllerContext, viewName, masterName, useCache);
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }

    public class MyView : IView
    {
        public object Model;
        public MyView(object model)
        {
            Model = model;
        }

        public void Render(ViewContext viewContext, TextWriter textWriter)
        {
            string template = "Hello @Model.Name! Welcome to Razor!";
            string result = RazorEngine.Razor.Parse(template, new { Name = "World" });
            string temp =
            @"@{
    Layout = ""~/Views/Shared/_Layout.cshtml"";
}
        Email: @Model.Hierarchy";

            var model = new PageModel
            {
                Hierarchy = "aksdalskdjas asdasdsalds"
            };

            // For simplicity I used table for layout
            var templateService = new TemplateService();
            var emailHtmlBody = templateService.Parse(temp, model, null, null);
            textWriter.Write(emailHtmlBody);
        }
    }

    public class MyVirtualPathProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            return base.FileExists(virtualPath);
            if (base.FileExists(virtualPath))
            {
                return true;
            }
            if (virtualPath.Contains("test"))
                return true;
            var page = FindPage(virtualPath);
            return page != null;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var page = FindPage(virtualPath);
            if (page == null)
            {
                return base.GetFile(virtualPath);
            }
            return new MyVirtualFile(virtualPath, page);
        }

        private Page FindPage(string virtualPath)
        {
            var pageServices = HostContainer.GetInstance<IPageServices>();
            return pageServices.GetPage(virtualPath);
        }

        public class MyVirtualFile : VirtualFile
        {
            private byte[] data;

            internal byte[] GetBytes(string str)
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }

            public MyVirtualFile(string virtualPath, Page page)
                : base(virtualPath)
            {
                this.data = GetBytes(page.Content);
            }

            public override Stream Open()
            {
                return new MemoryStream(data);
            }
        }
    }
}
