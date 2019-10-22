namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HereWeGoAgain : DbMigration
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
                        Cidade = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Marketplace_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.Marketplace_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Marketplace_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Marketplaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Pro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pros", t => t.Pro_Id, cascadeDelete: true)
                .Index(t => t.Pro_Id);
            
            CreateTable(
                "dbo.Pros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailContact = c.String(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Team_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.EmailContact, unique: true)
                .Index(t => t.Team_Id)
                .Index(t => t.User_Id);
            
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Followed_Id = c.Int(nullable: false),
                        Follower_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Followed_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Follower_Id, cascadeDelete: true)
                .Index(t => t.Followed_Id)
                .Index(t => t.Follower_Id);
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Category_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Game_Id);
            
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Game_Id = c.Int(nullable: false),
                        Media_Id = c.Int(nullable: false),
                        TypeMedia_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.Media_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMedia_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Media_Id)
                .Index(t => t.TypeMedia_Id);
            
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
                "dbo.MarketplaceMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Marketplace_Id = c.Int(nullable: false),
                        Media_Id = c.Int(nullable: false),
                        TypeMedia_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.Marketplace_Id, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.Media_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMedia_Id, cascadeDelete: true)
                .Index(t => t.Marketplace_Id)
                .Index(t => t.Media_Id)
                .Index(t => t.TypeMedia_Id);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Cart_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Cart_Id)
                .Index(t => t.Product_Id);
            
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Marketplace_Id = c.Int(nullable: false),
                        TypeProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marketplaces", t => t.Marketplace_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeProducts", t => t.TypeProduct_Id, cascadeDelete: true)
                .Index(t => t.Marketplace_Id)
                .Index(t => t.TypeProduct_Id);
            
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
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Media_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        TypeMedia_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.Media_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMedia_Id, cascadeDelete: true)
                .Index(t => t.Media_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.TypeMedia_Id);
            
            CreateTable(
                "dbo.SubjectGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Game_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Subject_Id);
            
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
                "dbo.TeamGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Game_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.UserGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Game_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Media_Id = c.Int(nullable: false),
                        TypeMedia_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.Media_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.TypeMedia_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Media_Id)
                .Index(t => t.TypeMedia_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        UpdatedAt = c.DateTime(defaultValueSql: "current_timestamp"),
                        Subject_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubjects", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserSubjects", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.UserMedias", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserMedias", "TypeMedia_Id", "dbo.TypeMedias");
            DropForeignKey("dbo.UserMedias", "Media_Id", "dbo.Media");
            DropForeignKey("dbo.UserGames", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TeamGames", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TeamGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.SubjectGames", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.ProductMedias", "TypeMedia_Id", "dbo.TypeMedias");
            DropForeignKey("dbo.ProductMedias", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductMedias", "Media_Id", "dbo.Media");
            DropForeignKey("dbo.ProductCarts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "TypeProduct_Id", "dbo.TypeProducts");
            DropForeignKey("dbo.Products", "Marketplace_Id", "dbo.Marketplaces");
            DropForeignKey("dbo.ProductCarts", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.MarketplaceMedias", "TypeMedia_Id", "dbo.TypeMedias");
            DropForeignKey("dbo.MarketplaceMedias", "Media_Id", "dbo.Media");
            DropForeignKey("dbo.MarketplaceMedias", "Marketplace_Id", "dbo.Marketplaces");
            DropForeignKey("dbo.GameMedias", "TypeMedia_Id", "dbo.TypeMedias");
            DropForeignKey("dbo.GameMedias", "Media_Id", "dbo.Media");
            DropForeignKey("dbo.GameMedias", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Follows", "Follower_Id", "dbo.Users");
            DropForeignKey("dbo.Follows", "Followed_Id", "dbo.Users");
            DropForeignKey("dbo.Carts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Carts", "Marketplace_Id", "dbo.Marketplaces");
            DropForeignKey("dbo.Marketplaces", "Pro_Id", "dbo.Pros");
            DropForeignKey("dbo.Pros", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Pros", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Addresses", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSubjects", new[] { "User_Id" });
            DropIndex("dbo.UserSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.UserMedias", new[] { "User_Id" });
            DropIndex("dbo.UserMedias", new[] { "TypeMedia_Id" });
            DropIndex("dbo.UserMedias", new[] { "Media_Id" });
            DropIndex("dbo.UserGames", new[] { "User_Id" });
            DropIndex("dbo.UserGames", new[] { "Game_Id" });
            DropIndex("dbo.TeamGames", new[] { "Team_Id" });
            DropIndex("dbo.TeamGames", new[] { "Game_Id" });
            DropIndex("dbo.SubjectGames", new[] { "Subject_Id" });
            DropIndex("dbo.SubjectGames", new[] { "Game_Id" });
            DropIndex("dbo.ProductMedias", new[] { "TypeMedia_Id" });
            DropIndex("dbo.ProductMedias", new[] { "Product_Id" });
            DropIndex("dbo.ProductMedias", new[] { "Media_Id" });
            DropIndex("dbo.Products", new[] { "TypeProduct_Id" });
            DropIndex("dbo.Products", new[] { "Marketplace_Id" });
            DropIndex("dbo.ProductCarts", new[] { "Product_Id" });
            DropIndex("dbo.ProductCarts", new[] { "Cart_Id" });
            DropIndex("dbo.MarketplaceMedias", new[] { "TypeMedia_Id" });
            DropIndex("dbo.MarketplaceMedias", new[] { "Media_Id" });
            DropIndex("dbo.MarketplaceMedias", new[] { "Marketplace_Id" });
            DropIndex("dbo.Media", new[] { "URL" });
            DropIndex("dbo.GameMedias", new[] { "TypeMedia_Id" });
            DropIndex("dbo.GameMedias", new[] { "Media_Id" });
            DropIndex("dbo.GameMedias", new[] { "Game_Id" });
            DropIndex("dbo.GameCategories", new[] { "Game_Id" });
            DropIndex("dbo.GameCategories", new[] { "Category_Id" });
            DropIndex("dbo.Follows", new[] { "Follower_Id" });
            DropIndex("dbo.Follows", new[] { "Followed_Id" });
            DropIndex("dbo.Pros", new[] { "User_Id" });
            DropIndex("dbo.Pros", new[] { "Team_Id" });
            DropIndex("dbo.Pros", new[] { "EmailContact" });
            DropIndex("dbo.Marketplaces", new[] { "Pro_Id" });
            DropIndex("dbo.Carts", new[] { "User_Id" });
            DropIndex("dbo.Carts", new[] { "Marketplace_Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropIndex("dbo.Addresses", new[] { "User_Id" });
            DropTable("dbo.UserSubjects");
            DropTable("dbo.UserMedias");
            DropTable("dbo.UserGames");
            DropTable("dbo.TeamGames");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectGames");
            DropTable("dbo.ProductMedias");
            DropTable("dbo.TypeProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCarts");
            DropTable("dbo.MarketplaceMedias");
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
            DropTable("dbo.Addresses");
        }
    }
}
