namespace DataLayer.Ioc
{
    using DataLayer;
    using SimpleInjector;

    public static class DataBootstraper
    {
        public static void RegisterServices(Container container)
        {
            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);
            container.Register<ICfe4Repository, Cfe4Repository>(Lifestyle.Scoped);
        }
    }
}