using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PX.Business.Mvc.Environments;
using PX.Business.Mvc.ViewEngines.Razor;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Languages;
using PX.Business.Services.Localizes;
using PX.Business.Services.Menus;
using PX.Business.Services.News;
using PX.Business.Services.NewsCategories;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Pages;
using PX.Business.Services.SettingTypes;
using PX.Business.Services.Settings;
using PX.Business.Services.Testimonials;
using PX.Business.Services.UserGroups;
using PX.Business.Services.Users;
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

            //Initialize some part of core
            InitializeProcess();

            //ViewEngines.Engines.Add(new ViewEngine());
            HostingEnvironment.RegisterVirtualPathProvider(new MyVirtualPathProvider());
        }

        #region Initialize Process

        public void InitializeProcess()
        {
            //Load localize resources
            LocalizedResourcesInitialize();

            //Initialize menu permissions
            MenuPermissionsInitialize();
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
            container.Register<ILocalizedResourceServices, LocalizedResourceServices>(Lifestyle.Singleton);
            container.Register<IMenuServices, MenuServices>(Lifestyle.Singleton);
            container.Register<IUserServices, UserServices>(Lifestyle.Singleton);
            container.Register<ILanguageServices, LanguageServices>(Lifestyle.Singleton);
            container.Register<ISettingServices, SettingServices>(Lifestyle.Singleton);
            container.Register<IUserGroupServices, UserGroupServices>(Lifestyle.Singleton);
            container.Register<IPageServices, PageServices>(Lifestyle.Singleton);
            container.Register<IPageTemplateServices, PageTemplateServices>(Lifestyle.Singleton);
            container.Register<ICurlyBracketServices, CurlyBracketServices>(Lifestyle.Singleton);
            container.Register<ITestimonialServices, TestimonialServices>(Lifestyle.Singleton);
            container.Register<ISettingTypeServices, SettingTypeServices>(Lifestyle.Singleton);
            container.Register<INewsServices, NewsServices>(Lifestyle.Singleton);
            container.Register<INewsCategoryServices, NewsCategorieservices>(Lifestyle.Singleton);
        }

        #endregion
    }
}