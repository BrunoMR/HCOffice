namespace Identity.Data
{
    using System.Data.Entity;
    using Dto;
    using EntityConfiguration;

    /// <summary>
    /// The identity context.
    /// </summary>
    public class IdentityContext : DbContext
    {
        public IdentityContext()
            : base("SqlConn")
        { }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new UserRoleConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
