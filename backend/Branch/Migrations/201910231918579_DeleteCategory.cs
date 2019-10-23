namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            DropIndex("dbo.GameCategories", new[] { "GameId" });
            DropIndex("dbo.GameCategories", new[] { "CategoryId" });
            CreateTable(
                "dbo.UserMarketplaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        MarketplaceId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.MarketplaceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MarketplaceId);
            
            DropColumn("dbo.SubjectGames", "Affinity");
            DropTable("dbo.Categories");
            DropTable("dbo.GameCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SubjectGames", "Affinity", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserMarketplaces", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMarketplaces", "MarketplaceId", "dbo.Marketplaces");
            DropIndex("dbo.UserMarketplaces", new[] { "MarketplaceId" });
            DropIndex("dbo.UserMarketplaces", new[] { "UserId" });
            DropTable("dbo.UserMarketplaces");
            CreateIndex("dbo.GameCategories", "CategoryId");
            CreateIndex("dbo.GameCategories", "GameId");
            AddForeignKey("dbo.GameCategories", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameCategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
