namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProMarketplace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pros", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Pros", "UserId", "dbo.Users");
            DropForeignKey("dbo.Marketplaces", "ProId", "dbo.Pros");
            DropForeignKey("dbo.Carts", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.GameMedias", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.GameMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.GameTeams", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.MarketplaceMedias", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.MarketplaceMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.MarketplaceMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.Products", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.Products", "TypeProductId", "dbo.TypeProducts");
            DropForeignKey("dbo.ProductMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.ProductMedias", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.SubjectGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.SubjectGames", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.UserGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.UserGames", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMarketplaces", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.UserMarketplaces", "UserId", "dbo.Users");
            DropIndex("dbo.Carts", new[] { "MarketplaceId" });
            DropIndex("dbo.Marketplaces", new[] { "ProId" });
            DropIndex("dbo.Pros", new[] { "EmailContact" });
            DropIndex("dbo.Pros", new[] { "UserId" });
            DropIndex("dbo.Pros", new[] { "TeamId" });
            DropIndex("dbo.GameMedias", new[] { "MediaId" });
            DropIndex("dbo.GameMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.GameMedias", new[] { "GameId" });
            DropIndex("dbo.GameTeams", new[] { "GameId" });
            DropIndex("dbo.GameTeams", new[] { "TeamId" });
            DropIndex("dbo.MarketplaceMedias", new[] { "MediaId" });
            DropIndex("dbo.MarketplaceMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.MarketplaceMedias", new[] { "MarketplaceId" });
            DropIndex("dbo.Products", new[] { "TypeProductId" });
            DropIndex("dbo.Products", new[] { "MarketplaceId" });
            DropIndex("dbo.ProductMedias", new[] { "MediaId" });
            DropIndex("dbo.ProductMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.ProductMedias", new[] { "ProductId" });
            DropIndex("dbo.SubjectGames", new[] { "SubjectId" });
            DropIndex("dbo.SubjectGames", new[] { "GameId" });
            DropIndex("dbo.UserGames", new[] { "UserId" });
            DropIndex("dbo.UserGames", new[] { "GameId" });
            DropIndex("dbo.UserMarketplaces", new[] { "UserId" });
            DropIndex("dbo.UserMarketplaces", new[] { "MarketplaceId" });
            AddColumn("dbo.Users", "IsPro", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Carts", "ProId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ProId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ProId");
            CreateIndex("dbo.Products", "ProId");
            AddForeignKey("dbo.Carts", "ProId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ProId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "MarketplaceId");
            DropColumn("dbo.Products", "TypeProductId");
            DropColumn("dbo.Products", "MarketplaceId");
            DropTable("dbo.Marketplaces");
            DropTable("dbo.Pros");
            DropTable("dbo.Teams");
            DropTable("dbo.GameMedias");
            DropTable("dbo.Games");
            DropTable("dbo.GameTeams");
            DropTable("dbo.MarketplaceMedias");
            DropTable("dbo.TypeProducts");
            DropTable("dbo.ProductMedias");
            DropTable("dbo.SubjectGames");
            DropTable("dbo.UserGames");
            DropTable("dbo.UserMarketplaces");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaId = c.Int(nullable: false),
                        TypeMediaId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarketplaceMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaId = c.Int(nullable: false),
                        TypeMediaId = c.Int(nullable: false),
                        MarketplaceId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Abbreviation = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaId = c.Int(nullable: false),
                        TypeMediaId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailContact = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marketplaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ProId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "MarketplaceId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "TypeProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "MarketplaceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "ProId", "dbo.Users");
            DropForeignKey("dbo.Carts", "ProId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "ProId" });
            DropIndex("dbo.Carts", new[] { "ProId" });
            DropColumn("dbo.Products", "ProId");
            DropColumn("dbo.Carts", "ProId");
            DropColumn("dbo.Users", "IsPro");
            CreateIndex("dbo.UserMarketplaces", "MarketplaceId");
            CreateIndex("dbo.UserMarketplaces", "UserId");
            CreateIndex("dbo.UserGames", "GameId");
            CreateIndex("dbo.UserGames", "UserId");
            CreateIndex("dbo.SubjectGames", "GameId");
            CreateIndex("dbo.SubjectGames", "SubjectId");
            CreateIndex("dbo.ProductMedias", "ProductId");
            CreateIndex("dbo.ProductMedias", "TypeMediaId");
            CreateIndex("dbo.ProductMedias", "MediaId");
            CreateIndex("dbo.Products", "MarketplaceId");
            CreateIndex("dbo.Products", "TypeProductId");
            CreateIndex("dbo.MarketplaceMedias", "MarketplaceId");
            CreateIndex("dbo.MarketplaceMedias", "TypeMediaId");
            CreateIndex("dbo.MarketplaceMedias", "MediaId");
            CreateIndex("dbo.GameTeams", "TeamId");
            CreateIndex("dbo.GameTeams", "GameId");
            CreateIndex("dbo.GameMedias", "GameId");
            CreateIndex("dbo.GameMedias", "TypeMediaId");
            CreateIndex("dbo.GameMedias", "MediaId");
            CreateIndex("dbo.Pros", "TeamId");
            CreateIndex("dbo.Pros", "UserId");
            CreateIndex("dbo.Pros", "EmailContact", unique: true);
            CreateIndex("dbo.Marketplaces", "ProId");
            CreateIndex("dbo.Carts", "MarketplaceId");
            AddForeignKey("dbo.UserMarketplaces", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserMarketplaces", "MarketplaceId", "dbo.Marketplaces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserGames", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserGames", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubjectGames", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubjectGames", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductMedias", "TypeMediaId", "dbo.TypeMedias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductMedias", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductMedias", "MediaId", "dbo.Media", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "TypeProductId", "dbo.TypeProducts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "MarketplaceId", "dbo.Marketplaces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MarketplaceMedias", "TypeMediaId", "dbo.TypeMedias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MarketplaceMedias", "MediaId", "dbo.Media", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MarketplaceMedias", "MarketplaceId", "dbo.Marketplaces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameTeams", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameTeams", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameMedias", "TypeMediaId", "dbo.TypeMedias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameMedias", "MediaId", "dbo.Media", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameMedias", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "MarketplaceId", "dbo.Marketplaces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Marketplaces", "ProId", "dbo.Pros", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pros", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pros", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
