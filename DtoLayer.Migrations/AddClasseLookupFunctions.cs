namespace DtoLayer.Migrations
{
    using FluentMigrator;

    [Migration(2)]
    public class AddClasseLookupFunctions : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript(@"GetJustClasse.sql");
            Execute.EmbeddedScript(@"GetJustEditionClasse.sql");
            Execute.EmbeddedScript(@"GetJustSubClasse.sql");
        }

        public override void Down()
        {
            Execute.EmbeddedScript(@"DropGetJustClasse.sql");
            Execute.EmbeddedScript(@"DropGetJustEditionClasse.sql");
            Execute.EmbeddedScript(@"DropGetJustSubClasse.sql");
        }
    }
}
