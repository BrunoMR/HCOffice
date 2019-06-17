namespace Identity.Data.EntityConfiguration
{
    using System.Data.Entity.ModelConfiguration;

    using Identity.Dto;

    class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(512);

            ToTable("AspNetRoles");
        }
    }
}
