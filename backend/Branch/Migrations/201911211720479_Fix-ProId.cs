namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixProId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "ProId" });
            AlterColumn("dbo.Products", "ProId", c => c.Int());
            CreateIndex("dbo.Products", "ProId");
            AddForeignKey("dbo.Products", "ProId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "ProId" });
            AlterColumn("dbo.Products", "ProId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProId");
            AddForeignKey("dbo.Products", "ProId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
