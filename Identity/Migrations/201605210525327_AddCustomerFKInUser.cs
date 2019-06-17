namespace Identity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerFKInUser : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("AspNetUsers", "CustomerId", "CLIENTE", "Id", 
                cascadeDelete: false, name: "FK_AspNetUsers_CustomerId");

            CreateIndex("AspNetUsers", "CustomerId", name: "IX_CustomerId");
        }
        
        public override void Down()
        {
            DropIndex("AspNetUsers", "IX_CustomerId");

            DropForeignKey("AspNetUsers", "FK_AspNetUsers_CustomerId");
        }
    }
}
