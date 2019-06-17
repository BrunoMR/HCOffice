namespace Identity.Extensions
{
    using Model;
    using Microsoft.AspNet.Identity;

    public static class UserManagerExtensions
    {
        /// <summary>
        /// The create if not exists.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public static void CreateIfNotExists(this UserManager<ApplicationUser> userManager, ApplicationUser user, string password)
        {
            if (userManager.FindByName(user.UserName) == null && 
                userManager.FindByEmail(user.Email) == null)
            {
                userManager.Create(user, password);
            }
        }

        public static void CreateIfNotExists(
            this UserManager<ApplicationUser> userManager,
            string username,
            string password,
            string role)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = username,
                EmailConfirmed = true
            };

            userManager.CreateIfNotExists(user, password);

            var persistedUser = userManager.FindByName(username);
            if (persistedUser == null)
            {
                return;
            }

            userManager.AddToRole(persistedUser.Id, role);
        }
    }
}
