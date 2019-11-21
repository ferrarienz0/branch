namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "MediaId", c => c.Int());
            CreateIndex("dbo.Products", "MediaId");
            AddForeignKey("dbo.Products", "MediaId", "dbo.Media", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MediaId", "dbo.Media");
            DropIndex("dbo.Products", new[] { "MediaId" });
            DropColumn("dbo.Products", "MediaId");
        }
    }
}
