namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAddress : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            AlterColumn("dbo.Addresses", "City_Id", c => c.Int());
            CreateIndex("dbo.Addresses", "City_Id");
            AddForeignKey("dbo.Addresses", "City_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            AlterColumn("dbo.Addresses", "City_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "City_Id");
            AddForeignKey("dbo.Addresses", "City_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
