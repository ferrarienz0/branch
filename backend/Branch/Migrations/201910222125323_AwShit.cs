namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AwShit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CEP = c.String(nullable: false),
                        Logradouro = c.String(nullable: false),
                        Complemento = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        EstateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estates", t => t.EstateId, cascadeDelete: true)
                .Index(t => t.EstateId);
            
            CreateTable(
                "dbo.Estates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Nickname = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Nickname, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Finished = c.Boolean(nullable: false),
                        Total = c.Single(nullable: false),
                        MarketplaceId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.MarketplaceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MarketplaceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Marketplaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ProId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pros", t => t.ProId, cascadeDelete: true)
                .Index(t => t.ProId);
            
            CreateTable(
                "dbo.Pros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailContact = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.EmailContact, unique: true)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Followed_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Followed_Id)
                .ForeignKey("dbo.Users", t => t.FollowerId, cascadeDelete: true)
                .Index(t => t.FollowerId)
                .Index(t => t.Followed_Id);
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Abbreviation = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMediaId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.TypeMediaId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URL = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.URL, unique: true);
            
            CreateTable(
                "dbo.TypeMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.MarketplaceMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaId = c.Int(nullable: false),
                        TypeMediaId = c.Int(nullable: false),
                        MarketplaceId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.MarketplaceId, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMediaId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.TypeMediaId)
                .Index(t => t.MarketplaceId);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CartId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Single(nullable: false),
                        Stock = c.Int(nullable: false),
                        CurrentDiscount = c.Single(nullable: false),
                        MaxDiscount = c.Single(nullable: false),
                        TypeProductId = c.Int(nullable: false),
                        MarketplaceId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.MarketplaceId, cascadeDelete: true)
                .ForeignKey("dbo.TypeProducts", t => t.TypeProductId, cascadeDelete: true)
                .Index(t => t.TypeProductId)
                .Index(t => t.MarketplaceId);
            
            CreateTable(
                "dbo.TypeProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMediaId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.TypeMediaId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SubjectGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hashtag = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.UserMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaId = c.Int(nullable: false),
                        TypeMediaId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMediaId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.TypeMediaId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.UserMedias", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.UserMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.UserGames", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.SubjectGames", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.ProductMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.ProductMedias", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.ProductCarts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "TypeProductId", "dbo.TypeProducts");
            DropForeignKey("dbo.Products", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.ProductCarts", "CartId", "dbo.Carts");
            DropForeignKey("dbo.MarketplaceMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.MarketplaceMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.MarketplaceMedias", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.GameTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.GameTeams", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.GameMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.GameMedias", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Follows", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.Follows", "Followed_Id", "dbo.Users");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Carts", "MarketplaceId", "dbo.Marketplaces");
            DropForeignKey("dbo.Marketplaces", "ProId", "dbo.Pros");
            DropForeignKey("dbo.Pros", "UserId", "dbo.Users");
            DropForeignKey("dbo.Pros", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "EstateId", "dbo.Estates");
            DropIndex("dbo.UserSubjects", new[] { "SubjectId" });
            DropIndex("dbo.UserSubjects", new[] { "UserId" });
            DropIndex("dbo.UserMedias", new[] { "UserId" });
            DropIndex("dbo.UserMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.UserMedias", new[] { "MediaId" });
            DropIndex("dbo.UserGames", new[] { "GameId" });
            DropIndex("dbo.UserGames", new[] { "UserId" });
            DropIndex("dbo.SubjectGames", new[] { "GameId" });
            DropIndex("dbo.SubjectGames", new[] { "SubjectId" });
            DropIndex("dbo.ProductMedias", new[] { "ProductId" });
            DropIndex("dbo.ProductMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.ProductMedias", new[] { "MediaId" });
            DropIndex("dbo.Products", new[] { "MarketplaceId" });
            DropIndex("dbo.Products", new[] { "TypeProductId" });
            DropIndex("dbo.ProductCarts", new[] { "CartId" });
            DropIndex("dbo.ProductCarts", new[] { "ProductId" });
            DropIndex("dbo.MarketplaceMedias", new[] { "MarketplaceId" });
            DropIndex("dbo.MarketplaceMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.MarketplaceMedias", new[] { "MediaId" });
            DropIndex("dbo.GameTeams", new[] { "TeamId" });
            DropIndex("dbo.GameTeams", new[] { "GameId" });
            DropIndex("dbo.Media", new[] { "URL" });
            DropIndex("dbo.GameMedias", new[] { "GameId" });
            DropIndex("dbo.GameMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.GameMedias", new[] { "MediaId" });
            DropIndex("dbo.GameCategories", new[] { "CategoryId" });
            DropIndex("dbo.GameCategories", new[] { "GameId" });
            DropIndex("dbo.Follows", new[] { "Followed_Id" });
            DropIndex("dbo.Follows", new[] { "FollowerId" });
            DropIndex("dbo.Pros", new[] { "TeamId" });
            DropIndex("dbo.Pros", new[] { "UserId" });
            DropIndex("dbo.Pros", new[] { "EmailContact" });
            DropIndex("dbo.Marketplaces", new[] { "ProId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.Carts", new[] { "MarketplaceId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropIndex("dbo.Cities", new[] { "EstateId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.UserSubjects");
            DropTable("dbo.UserMedias");
            DropTable("dbo.UserGames");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectGames");
            DropTable("dbo.ProductMedias");
            DropTable("dbo.TypeProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCarts");
            DropTable("dbo.MarketplaceMedias");
            DropTable("dbo.GameTeams");
            DropTable("dbo.TypeMedias");
            DropTable("dbo.Media");
            DropTable("dbo.GameMedias");
            DropTable("dbo.Games");
            DropTable("dbo.GameCategories");
            DropTable("dbo.Follows");
            DropTable("dbo.Categories");
            DropTable("dbo.Teams");
            DropTable("dbo.Pros");
            DropTable("dbo.Marketplaces");
            DropTable("dbo.Carts");
            DropTable("dbo.Users");
            DropTable("dbo.Estates");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
