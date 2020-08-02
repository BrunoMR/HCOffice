using ExtratorDeDados.Importer;
using SimpleInjector;

namespace ExtratorDeDados.Ioc
{
    public static class ExtratorBootstraper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<IImportFile, ImportFile>(Lifestyle.Scoped);
        }
    }
}