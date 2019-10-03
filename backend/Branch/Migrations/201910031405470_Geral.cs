namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Geral : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CEP = c.String(nullable: false),
                        Logradouro = c.String(nullable: false),
                        Complemento = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        Cidade = c.String(nullable: false),
                        IDUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.IDUser_ID, cascadeDelete: true)
                .Index(t => t.IDUser_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Nickname = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Nickname, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Finished = c.Boolean(nullable: false),
                        Total = c.Single(nullable: false),
                        IDMarketplace_ID = c.Int(nullable: false),
                        IDUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Marketplaces", t => t.IDMarketplace_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IDUser_ID, cascadeDelete: true)
                .Index(t => t.IDMarketplace_ID)
                .Index(t => t.IDUser_ID);
            
            CreateTable(
                "dbo.Marketplaces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IDPro_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pros", t => t.IDPro_ID, cascadeDelete: true)
                .Index(t => t.IDPro_ID);
            
            CreateTable(
                "dbo.Pros",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmailContact = c.String(nullable: false),
                        IDTeam_ID = c.Int(nullable: false),
                        IDUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Teams", t => t.IDTeam_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IDUser_ID, cascadeDelete: true)
                .Index(t => t.EmailContact, unique: true)
                .Index(t => t.IDTeam_ID)
                .Index(t => t.IDUser_ID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        IDFollowed_ID = c.Int(nullable: false),
                        IDFollower_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.IDFollowed_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IDFollower_ID, cascadeDelete: true)
                .Index(t => t.IDFollowed_ID)
                .Index(t => t.IDFollower_ID);
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDCategory_ID = c.Int(nullable: false),
                        IDGame_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.IDCategory_ID, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.IDGame_ID, cascadeDelete: true)
                .Index(t => t.IDCategory_ID)
                .Index(t => t.IDGame_ID);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Abbreviation = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GameMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDGame_ID = c.Int(nullable: false),
                        IDMedia_ID = c.Int(nullable: false),
                        IDTypeMedia_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.IDGame_ID, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.IDMedia_ID, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.IDTypeMedia_ID, cascadeDelete: true)
                .Index(t => t.IDGame_ID)
                .Index(t => t.IDMedia_ID)
                .Index(t => t.IDTypeMedia_ID);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.URL, unique: true);
            
            CreateTable(
                "dbo.TypeMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MarketplaceMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMarketplace_ID = c.Int(nullable: false),
                        IDMedia_ID = c.Int(nullable: false),
                        IDTypeMedia_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Marketplaces", t => t.IDMarketplace_ID, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.IDMedia_ID, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.IDTypeMedia_ID, cascadeDelete: true)
                .Index(t => t.IDMarketplace_ID)
                .Index(t => t.IDMedia_ID)
                .Index(t => t.IDTypeMedia_ID);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        IDCart_ID = c.Int(nullable: false),
                        IDProduct_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carts", t => t.IDCart_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.IDProduct_ID, cascadeDelete: true)
                .Index(t => t.IDCart_ID)
                .Index(t => t.IDProduct_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Single(nullable: false),
                        Stock = c.Int(nullable: false),
                        CurrentDiscount = c.Single(nullable: false),
                        MaxDiscount = c.Single(nullable: false),
                        IDMarketplace_ID = c.Int(nullable: false),
                        IDTypeProduct_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Marketplaces", t => t.IDMarketplace_ID, cascadeDelete: true)
                .ForeignKey("dbo.TypeProducts", t => t.IDTypeProduct_ID, cascadeDelete: true)
                .Index(t => t.IDMarketplace_ID)
                .Index(t => t.IDTypeProduct_ID);
            
            CreateTable(
                "dbo.TypeProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMedia_ID = c.Int(nullable: false),
                        IDProduct_ID = c.Int(nullable: false),
                        IDTypeMedia_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Media", t => t.IDMedia_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.IDProduct_ID, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.IDTypeMedia_ID, cascadeDelete: true)
                .Index(t => t.IDMedia_ID)
                .Index(t => t.IDProduct_ID)
                .Index(t => t.IDTypeMedia_ID);
            
            CreateTable(
                "dbo.SubjectGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        IDGame_ID = c.Int(nullable: false),
                        IDSubject_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.IDGame_ID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.IDSubject_ID, cascadeDelete: true)
                .Index(t => t.IDGame_ID)
                .Index(t => t.IDSubject_ID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Hashtag = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeamGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDGame_ID = c.Int(nullable: false),
                        IDTeam_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.IDGame_ID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.IDTeam_ID, cascadeDelete: true)
                .Index(t => t.IDGame_ID)
                .Index(t => t.IDTeam_ID);
            
            CreateTable(
                "dbo.TeamMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMedia_ID = c.Int(nullable: false),
                        IDTeam_ID = c.Int(nullable: false),
                        IDTypeMedia_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Media", t => t.IDMedia_ID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.IDTeam_ID, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.IDTypeMedia_ID, cascadeDelete: true)
                .Index(t => t.IDMedia_ID)
                .Index(t => t.IDTeam_ID)
                .Index(t => t.IDTypeMedia_ID);
            
            CreateTable(
                "dbo.UserGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        IDGame_ID = c.Int(nullable: false),
                        IDUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.IDGame_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IDUser_ID, cascadeDelete: true)
                .Index(t => t.IDGame_ID)
                .Index(t => t.IDUser_ID);
            
            CreateTable(
                "dbo.UserMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMedia_ID = c.Int(nullable: false),
                        IDTypeMedia_ID = c.Int(nullable: false),
                        IDUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Media", t => t.IDMedia_ID, cascadeDelete: true)
                .ForeignKey("dbo.TypeMedias", t => t.IDTypeMedia_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IDUser_ID, cascadeDelete: true)
                .Index(t => t.IDMedia_ID)
                .Index(t => t.IDTypeMedia_ID)
                .Index(t => t.IDUser_ID);
            
            CreateTable(
                "dbo.UserSubjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Affinity = c.Int(nullable: false),
                        IDSubject_ID = c.Int(nullable: false),
                        IDUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Subjects", t => t.IDSubject_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IDUser_ID, cascadeDelete: true)
                .Index(t => t.IDSubject_ID)
                .Index(t => t.IDUser_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubjects", "IDUser_ID", "dbo.Users");
            DropForeignKey("dbo.UserSubjects", "IDSubject_ID", "dbo.Subjects");
            DropForeignKey("dbo.UserMedias", "IDUser_ID", "dbo.Users");
            DropForeignKey("dbo.UserMedias", "IDTypeMedia_ID", "dbo.TypeMedias");
            DropForeignKey("dbo.UserMedias", "IDMedia_ID", "dbo.Media");
            DropForeignKey("dbo.UserGames", "IDUser_ID", "dbo.Users");
            DropForeignKey("dbo.UserGames", "IDGame_ID", "dbo.Games");
            DropForeignKey("dbo.TeamMedias", "IDTypeMedia_ID", "dbo.TypeMedias");
            DropForeignKey("dbo.TeamMedias", "IDTeam_ID", "dbo.Teams");
            DropForeignKey("dbo.TeamMedias", "IDMedia_ID", "dbo.Media");
            DropForeignKey("dbo.TeamGames", "IDTeam_ID", "dbo.Teams");
            DropForeignKey("dbo.TeamGames", "IDGame_ID", "dbo.Games");
            DropForeignKey("dbo.SubjectGames", "IDSubject_ID", "dbo.Subjects");
            DropForeignKey("dbo.SubjectGames", "IDGame_ID", "dbo.Games");
            DropForeignKey("dbo.ProductMedias", "IDTypeMedia_ID", "dbo.TypeMedias");
            DropForeignKey("dbo.ProductMedias", "IDProduct_ID", "dbo.Products");
            DropForeignKey("dbo.ProductMedias", "IDMedia_ID", "dbo.Media");
            DropForeignKey("dbo.ProductCarts", "IDProduct_ID", "dbo.Products");
            DropForeignKey("dbo.Products", "IDTypeProduct_ID", "dbo.TypeProducts");
            DropForeignKey("dbo.Products", "IDMarketplace_ID", "dbo.Marketplaces");
            DropForeignKey("dbo.ProductCarts", "IDCart_ID", "dbo.Carts");
            DropForeignKey("dbo.MarketplaceMedias", "IDTypeMedia_ID", "dbo.TypeMedias");
            DropForeignKey("dbo.MarketplaceMedias", "IDMedia_ID", "dbo.Media");
            DropForeignKey("dbo.MarketplaceMedias", "IDMarketplace_ID", "dbo.Marketplaces");
            DropForeignKey("dbo.GameMedias", "IDTypeMedia_ID", "dbo.TypeMedias");
            DropForeignKey("dbo.GameMedias", "IDMedia_ID", "dbo.Media");
            DropForeignKey("dbo.GameMedias", "IDGame_ID", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "IDGame_ID", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "IDCategory_ID", "dbo.Categories");
            DropForeignKey("dbo.Follows", "IDFollower_ID", "dbo.Users");
            DropForeignKey("dbo.Follows", "IDFollowed_ID", "dbo.Users");
            DropForeignKey("dbo.Carts", "IDUser_ID", "dbo.Users");
            DropForeignKey("dbo.Carts", "IDMarketplace_ID", "dbo.Marketplaces");
            DropForeignKey("dbo.Marketplaces", "IDPro_ID", "dbo.Pros");
            DropForeignKey("dbo.Pros", "IDUser_ID", "dbo.Users");
            DropForeignKey("dbo.Pros", "IDTeam_ID", "dbo.Teams");
            DropForeignKey("dbo.Addresses", "IDUser_ID", "dbo.Users");
            DropIndex("dbo.UserSubjects", new[] { "IDUser_ID" });
            DropIndex("dbo.UserSubjects", new[] { "IDSubject_ID" });
            DropIndex("dbo.UserMedias", new[] { "IDUser_ID" });
            DropIndex("dbo.UserMedias", new[] { "IDTypeMedia_ID" });
            DropIndex("dbo.UserMedias", new[] { "IDMedia_ID" });
            DropIndex("dbo.UserGames", new[] { "IDUser_ID" });
            DropIndex("dbo.UserGames", new[] { "IDGame_ID" });
            DropIndex("dbo.TeamMedias", new[] { "IDTypeMedia_ID" });
            DropIndex("dbo.TeamMedias", new[] { "IDTeam_ID" });
            DropIndex("dbo.TeamMedias", new[] { "IDMedia_ID" });
            DropIndex("dbo.TeamGames", new[] { "IDTeam_ID" });
            DropIndex("dbo.TeamGames", new[] { "IDGame_ID" });
            DropIndex("dbo.SubjectGames", new[] { "IDSubject_ID" });
            DropIndex("dbo.SubjectGames", new[] { "IDGame_ID" });
            DropIndex("dbo.ProductMedias", new[] { "IDTypeMedia_ID" });
            DropIndex("dbo.ProductMedias", new[] { "IDProduct_ID" });
            DropIndex("dbo.ProductMedias", new[] { "IDMedia_ID" });
            DropIndex("dbo.Products", new[] { "IDTypeProduct_ID" });
            DropIndex("dbo.Products", new[] { "IDMarketplace_ID" });
            DropIndex("dbo.ProductCarts", new[] { "IDProduct_ID" });
            DropIndex("dbo.ProductCarts", new[] { "IDCart_ID" });
            DropIndex("dbo.MarketplaceMedias", new[] { "IDTypeMedia_ID" });
            DropIndex("dbo.MarketplaceMedias", new[] { "IDMedia_ID" });
            DropIndex("dbo.MarketplaceMedias", new[] { "IDMarketplace_ID" });
            DropIndex("dbo.Media", new[] { "URL" });
            DropIndex("dbo.GameMedias", new[] { "IDTypeMedia_ID" });
            DropIndex("dbo.GameMedias", new[] { "IDMedia_ID" });
            DropIndex("dbo.GameMedias", new[] { "IDGame_ID" });
            DropIndex("dbo.GameCategories", new[] { "IDGame_ID" });
            DropIndex("dbo.GameCategories", new[] { "IDCategory_ID" });
            DropIndex("dbo.Follows", new[] { "IDFollower_ID" });
            DropIndex("dbo.Follows", new[] { "IDFollowed_ID" });
            DropIndex("dbo.Pros", new[] { "IDUser_ID" });
            DropIndex("dbo.Pros", new[] { "IDTeam_ID" });
            DropIndex("dbo.Pros", new[] { "EmailContact" });
            DropIndex("dbo.Marketplaces", new[] { "IDPro_ID" });
            DropIndex("dbo.Carts", new[] { "IDUser_ID" });
            DropIndex("dbo.Carts", new[] { "IDMarketplace_ID" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropIndex("dbo.Addresses", new[] { "IDUser_ID" });
            DropTable("dbo.UserSubjects");
            DropTable("dbo.UserMedias");
            DropTable("dbo.UserGames");
            DropTable("dbo.TeamMedias");
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
