using PX.Business.Services.Menus;
using PX.Business.Services.Users;

[assembly: WebActivator.PostApplicationStartMethod(typeof(PX.Web.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace PX.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IMenuServices, MenuServices>(Lifestyle.Singleton);
            container.Register<IUserServices, UserServices>(Lifestyle.Singleton);
        }
    }
}