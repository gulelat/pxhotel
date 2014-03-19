using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PX.Business.Models.Localizes;
using PX.Business.Services.Languages;
using PX.Business.Services.Localizes;
using PX.Business.Services.Menus;
using PX.Business.Services.Settings;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Environment;
using PX.Core.Framework.Mvc.Helpers;
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

            //Load localize resources
            LocalizeResourcesInitialize();
        }

        public void LocalizeResourcesInitialize()
        {
            var localizeServices = DependencyFactory.GetInstance<ILocalizedResourceServices>();
            localizeServices.RefreshDictionary();
        }

        #region Simple Injector Initialize
        public static void SimpleInjectorInitialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();

            SimpleInjectorInitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            // Set the service locator here
            DependencyFactory.SetContainer(container);

        }

        private static void SimpleInjectorInitializeContainer(Container container)
        {
            container.Register<IMenuServices, MenuServices>(Lifestyle.Singleton);
            container.Register<IUserServices, UserServices>(Lifestyle.Singleton);
            container.Register<ISettingServices, SettingServices>(Lifestyle.Singleton);
            container.Register<ILocalizedResourceServices, LocalizedResourceServices>(Lifestyle.Singleton);
            container.Register<ILanguageServices, LanguageServices>(Lifestyle.Singleton);
        }

        #endregion
    }
}