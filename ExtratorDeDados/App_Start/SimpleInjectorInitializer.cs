using System.Web.Mvc;
using BusinessLayer.Ioc;
using DataLayer.Ioc;
using ExtratorDeDados;
using ExtratorDeDados.Ioc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]

namespace ExtratorDeDados
{
    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Chamada dos módulos do Simple Injector
            InitializeContainer(container);
            
            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            DataBootstraper.RegisterServices(container);
            BusinessBootstraper.RegisterServices(container);
            ExtratorBootstraper.RegisterServices(container);
        }
    }
}