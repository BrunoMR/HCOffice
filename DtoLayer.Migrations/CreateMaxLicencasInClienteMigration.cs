namespace DtoLayer.Migrations
{
    using FluentMigrator;

    [Migration(1)]
    public class CreateMaxLicencasInClienteMigration : Migration
    {
        public override void Up()
        {
            Create.Column("MAX_LICENCAS").OnTable("CLIENTE").AsInt32().Nullable();
        }

        public override void Down()
        {
            Delete.Column("MAX_LICENCAS").FromTable("CLIENTE");
        }
    }
}
