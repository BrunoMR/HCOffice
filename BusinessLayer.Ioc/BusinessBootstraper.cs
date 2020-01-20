namespace BusinessLayer.Ioc
{
    using SimpleInjector;

    public static class BusinessBootstraper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IClienteNegocio, ClienteNegocio>(Lifestyle.Scoped);
            //container.Register<ICfe4Negocio, Cfe4Negocio>(Lifestyle.Scoped);
        }
    }
}