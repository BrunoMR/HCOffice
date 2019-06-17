namespace SistemaWeb.Security
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using BusinessLayer;

    using Identity.Configuration;
    using Identity.Dto;
    using Identity.Model;

    using Microsoft.AspNet.Identity;

    public class OperatorUserManager : ApplicationUserManager
    {
        private readonly IClienteNegocio clienteNegocio;
        private readonly IUserRepository userRepository;

        public OperatorUserManager(
            IUserStore<ApplicationUser> store, 
            IClienteNegocio clienteNegocio,
            IUserRepository userRepository)
            : base(store)
        {
            this.clienteNegocio = clienteNegocio;
            this.userRepository = userRepository;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            Debug.Assert(user.CustomerId.HasValue, "user.CustomerId.HasValue is not true");

            if (await this.HasMaxNumberOfLicensesReached(user.CustomerId.Value))
            {
                return IdentityResult.Failed("Não há mais licenças disponíveis para criar este usuário.");
            }

            var result = await base.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var persistedUser = this.FindByName(user.UserName);
                Debug.Assert(persistedUser != null, "user.CustomerId.HasValue is null");
                await this.AddToRoleAsync(persistedUser.Id, "Operator");
            }

            return result;
        }
        
        private async Task<bool> HasMaxNumberOfLicensesReached(int customerId)
        {
            var customer = await this.clienteNegocio.FindByIdAsync(customerId);
            var operatorsCount = this.userRepository.GetByCustomerId(customerId).Count();
            return customer.MaxLicencas <= operatorsCount;
        } 
    }
}