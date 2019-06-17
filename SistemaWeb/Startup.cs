using Microsoft.Owin;
using Owin;
using SistemaWeb;

[assembly: OwinStartup(typeof(Startup))]
namespace SistemaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}