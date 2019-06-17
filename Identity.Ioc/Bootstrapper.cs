namespace Identity.Ioc
{
    using Configuration;
    using Context;
    using Data;
    using Dto;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Model;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;

    /// <summary>
    /// The bootstrapper.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>The register services.</summary>
        /// <param name="container">The container.</param>
        public static void RegisterServices(Container container)
        {
            //container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //container.Register<ApplicationDbContext>();
            //container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>());
            //container.Register<ApplicationRoleManager>();
            //container.Register<ApplicationUserManager>();
            //container.Register<ApplicationSignInManager>();
            //container.Register<IUserRepository, UserRepository>();

            container.RegisterPerWebRequest<ApplicationDbContext>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>());
            container.RegisterPerWebRequest<ApplicationRoleManager>();
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();

            container.RegisterPerWebRequest<IUserRepository, UserRepository>();
        }
    }
}
