namespace Identity.Migrations
{
    using Context;
    using Extensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System.Data.Entity.Migrations;
    using System;
    using System.Diagnostics;
    using System.Configuration;
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Identity.Context.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            this.CreateRoles();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            this.CreateRootUser(userManager);
            this.CreateTestUsers(userManager);
        }

        private void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            roleManager.CreateIfNotExists(new IdentityRole { Name = "Root" });
            roleManager.CreateIfNotExists(new IdentityRole { Name = "Admin" });
            roleManager.CreateIfNotExists(new IdentityRole { Name = "Customer" });
            roleManager.CreateIfNotExists(new IdentityRole { Name = "Operator" });
        }

        private void CreateRootUser(UserManager<ApplicationUser> userManager)
        {
            userManager.CreateIfNotExists(
                "root1212435121@root.com", 
                "074aa859bb5a49b58316978ecb04e81b", 
                "Root");
        }

        private void CreateTestUsers(UserManager<ApplicationUser> userManager)
        {
            bool createTestUsers;
            if (!bool.TryParse(ConfigurationManager.AppSettings["createTestUsers"], out createTestUsers))
            {
                return;
            }

            if (createTestUsers)
            {
                userManager.CreateIfNotExists(
                    "admin1212435121@admin.com",
                    "214a2e1cf94dfa8101222d54f8a19514",
                    "Admin");

                userManager.CreateIfNotExists(
                    "customer1212435121@customer.com",
                    "e1224296aa25468385b6fda222348b74",
                    "Customer");

                userManager.CreateIfNotExists(
                    "operator1212435121@operator.com",
                    "828922566aa074d2f8eb622996c2dad8b9a",
                    "Operator");
            }
        }
    }
}
