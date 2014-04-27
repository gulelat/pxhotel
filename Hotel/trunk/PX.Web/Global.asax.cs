using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PX.Business.Mvc.ViewEngines;
using PX.Business.Services.Banners;
using PX.Business.Services.ClientMenus;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.FileTemplates;
using PX.Business.Services.Languages;
using PX.Business.Services.Localizes;
using PX.Business.Services.Media;
using PX.Business.Services.Menus;
using PX.Business.Services.News;
using PX.Business.Services.NewsCategories;
using PX.Business.Services.PageLogs;
using PX.Business.Services.PageTemplateLogs;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Pages;
using PX.Business.Services.RotatingImageGroups;
using PX.Business.Services.RotatingImages;
using PX.Business.Services.Services;
using PX.Business.Services.SettingTypes;
using PX.Business.Services.Settings;
using PX.Business.Services.SQLTool;
using PX.Business.Services.Tags;
using PX.Business.Services.TemplateLogs;
using PX.Business.Services.Templates;
using PX.Business.Services.Testimonials;
using PX.Business.Services.UserGroups;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Environments;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace PX.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Initialize Simple Injector
            SimpleInjectorInitialize();

            //Initialize View Engine
            InitializeViewEngine();

            //Initialize some part of core
            InitializeProcess();

        }

        #region View Engine

        /// <summary>
        /// Initialize view engine
        /// </summary>
        private void InitializeViewEngine()
        {
            //Remove All Engine
            ViewEngines.Engines.Clear();
            //Add Razor Engine
            ViewEngines.Engines.Add(new RazorViewEngine());

            //ViewEngines.Engines.Add(new ViewEngine());
            HostingEnvironment.RegisterVirtualPathProvider(new MyVirtualPathProvider());
        }

        #endregion

        #region Initialize Process

        public void InitializeProcess()
        {
            //Setting Initialize
            SettingInitialize();

            //Load localize resources
            LocalizedResourcesInitialize();

            //Initialize menu permissions
            MenuPermissionsInitialize();

            //Initialize File Template
            FileTemplateInitialize();

            //Initialize default template for curly bracket
            TemplatesInitialize();
        }

        /// <summary>
        /// Initialize localized resources
        /// </summary>
        public void SettingInitialize()
        {
            var settingServices = HostContainer.GetInstance<ISettingServices>();
            settingServices.Initialize();
        }

        /// <summary>
        /// Initialize localized resources
        /// </summary>
        public void LocalizedResourcesInitialize()
        {
            var localizeServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            localizeServices.RefreshDictionary();
        }

        /// <summary>
        /// Initialize menu permissions
        /// </summary>
        public void MenuPermissionsInitialize()
        {
            var menuServices = HostContainer.GetInstance<IMenuServices>();
            menuServices.InitializeMenuPermissions();
        }

        /// <summary>
        /// Initialize menu permissions
        /// </summary>
        public void FileTemplateInitialize()
        {
            var pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            pageTemplateServices.InitializeFileTemplates();
        }

        public void TemplatesInitialize()
        {
            var templateServices = HostContainer.GetInstance<ITemplateServices>();
            templateServices.InitializeTemplates();
        }
        
        #endregion

        #region Simple Injector Initialize
        /// <summary>
        /// Simple injector initialize
        /// </summary>
        public static void SimpleInjectorInitialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();

            SimpleInjectorInitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // Set the service locator here
            HostContainer.SetContainer(container);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));


        }

        /// <summary>
        /// Initialize all required interface
        /// </summary>
        /// <param name="container"></param>
        private static void SimpleInjectorInitializeContainer(Container container)
        {
            //container.Register(() => new PXHotelEntities(), Lifestyle.Transient);
            container.Register<ILocalizedResourceServices, LocalizedResourceServices>(Lifestyle.Transient);
            container.Register<IMenuServices, MenuServices>(Lifestyle.Transient);
            container.Register<IUserServices, UserServices>(Lifestyle.Transient);
            container.Register<ILanguageServices, LanguageServices>(Lifestyle.Transient);
            container.Register<ISettingServices, SettingServices>(Lifestyle.Transient);
            container.Register<IUserGroupServices, UserGroupServices>(Lifestyle.Transient);
            container.Register<IPageServices, PageServices>(Lifestyle.Transient);
            container.Register<IPageTemplateServices, PageTemplateServices>(Lifestyle.Transient);
            container.Register<ICurlyBracketServices, CurlyBracketServices>(Lifestyle.Transient);
            container.Register<ITestimonialServices, TestimonialServices>(Lifestyle.Transient);
            container.Register<ISettingTypeServices, SettingTypeServices>(Lifestyle.Transient);
            container.Register<INewsServices, NewsServices>(Lifestyle.Transient);
            container.Register<INewsCategoryServices, NewsCategorieservices>(Lifestyle.Transient);
            container.Register<ITemplateServices, TemplateServices>(Lifestyle.Transient);
            container.Register<IRotatingImageServices, RotatingImageServices>(Lifestyle.Transient);
            container.Register<IRotatingImageGroupServices, RotatingImageGroupServices>(Lifestyle.Transient);
            container.Register<IMediaServices, MediaServices>(Lifestyle.Singleton);
            container.Register<IMediaFileManager, MediaFileManager>(Lifestyle.Transient);
            container.Register<ITagServices, TagServices>(Lifestyle.Transient);
            container.Register<IClientMenuServices, ClientMenuServices>(Lifestyle.Transient);
            container.Register<IFileTemplateServices, FileTemplateServices>(Lifestyle.Transient);
            container.Register<IPageLogServices, PageLogServices>(Lifestyle.Transient);
            container.Register<IPageTemplateLogServices, PageTemplateLogServices>(Lifestyle.Transient);
            container.Register<ITemplateLogServices, TemplateLogServices>(Lifestyle.Transient);
            container.Register<ISQLCommandServices, SQLCommandServices>(Lifestyle.Transient);
            container.Register<IBannerServices, BannerServices>(Lifestyle.Transient);
            container.Register<IServiceServices, ServiceServices>(Lifestyle.Transient);
        }

        #endregion
    }
}