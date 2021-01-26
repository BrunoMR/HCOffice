using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Ioc
{
    using SimpleInjector;

    public static class BusinessBootstraper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IClienteNegocio, ClienteNegocio>(Lifestyle.Scoped);
            container.Register<ICfe4Negocio, Cfe4Negocio>(Lifestyle.Scoped);
            container.Register<IRpiNegocio, RpiNegocio>(Lifestyle.Scoped);
            container.Register<ITitularBusiness, TitularBusiness>(Lifestyle.Scoped);
        }
    }
}