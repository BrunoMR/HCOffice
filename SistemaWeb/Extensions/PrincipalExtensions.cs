namespace SistemaWeb.Extensions
{
    using System.Security.Principal;

    public static class PrincipalExtensions
    {
        public static bool HasAnyRoles(this IPrincipal principal, string roles)
        {
            var rolesList = roles.Split(',');
            foreach (var role in rolesList)
            {
                if (principal.IsInRole(role.Trim()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}