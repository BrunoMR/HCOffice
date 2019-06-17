namespace SistemaWeb.Extensions
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Identity.Configuration;

    public static class ApplicationUserManagerExtensions
    {
        public static async Task<int> GetCustomerId(this ApplicationUserManager applicationUserManager, string userId)
        {
            var user = await applicationUserManager.FindByIdAsync(userId);

            Debug.Assert(user.CustomerId.HasValue, "user.CustomerId.HasValue is not true");

            return user.CustomerId.Value;
        }
    }
}