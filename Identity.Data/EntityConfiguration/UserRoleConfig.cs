namespace Identity.Data.EntityConfiguration
{
    using System.Data.Entity.ModelConfiguration;
    using Dto;

    class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfig()
        {
            HasKey(u => new { u.UserId, u.RoleId });

            Property(u => u.UserId)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.RoleId)
                .IsRequired()
                .HasMaxLength(256);

            HasRequired(p => p.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(p => p.UserId);

            HasRequired(p => p.Role)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(p => p.RoleId);

            ToTable("AspNetUserRoles");
        }
    }
}
