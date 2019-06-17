namespace Identity.Configuration
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security.DataProtection;
    using Model;

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            // Username validation paramaters
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Password validation parameters
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Lockout configuration
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Two Factor Autentication Providers
            RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Defining Email service class
            EmailService = new EmailService();

            // Defining SMS service class
            // SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("IdentityPropriedadeIntelectual");
            var dataProtector = provider.Create("ASP.NET Identity");

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);

        }

        public async Task<IdentityResult> UpsertAsync(ApplicationUser user, string password)
        {
            var applicationUser = await this.FindByEmailAsync(user.Email);
            if (applicationUser == null)
            {
                var result = await CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var persistedUser = this.FindByName(user.UserName);
                    await this.AddToRoleAsync(persistedUser.Id, "Customer");
                }

                return result;
            }
            else
            {
                applicationUser.Email = user.Email;
                applicationUser.UserName = user.Email;

                return await UpdateAsync(applicationUser);
            }
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var result = await base.CreateAsync(user, password);
            return result;
        }

        public override async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            var result = await base.DeleteAsync(user);
            return result;
        }
    }
}