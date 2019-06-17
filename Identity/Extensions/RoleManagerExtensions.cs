namespace Identity.Extensions
{
    using Microsoft.AspNet.Identity;

    public static class RoleManagerExtensions
    {
        public static void CreateIfNotExists<T>(this RoleManager<T> roleManager, T role) where T : class, IRole<string>
        {
            if (!roleManager.RoleExists(role.Name))
            {
                roleManager.Create(role);
            }
        }
    }
}
