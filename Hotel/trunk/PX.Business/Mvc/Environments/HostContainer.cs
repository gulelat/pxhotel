using SimpleInjector;

namespace PX.Business.Mvc.Environments
{
    public static class HostContainer
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
