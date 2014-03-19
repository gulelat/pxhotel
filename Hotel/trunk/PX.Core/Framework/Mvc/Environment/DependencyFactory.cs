using SimpleInjector;

namespace PX.Core.Framework.Mvc.Environment
{
    // Service Locator implementation in low application layer.
    public static class DependencyFactory
    {
        private static Container _container;

        public static void SetContainer(Container container)
        {
            _container = container;
        }

        public static T GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}
