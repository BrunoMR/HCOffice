namespace Identity.Dto
{
    using System.Collections.Generic;

    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
