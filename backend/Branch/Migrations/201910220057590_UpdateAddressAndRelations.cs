namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAddressAndRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.GameCategories", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TeamGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TeamGames", "Team_Id", "dbo.Teams");
            DropIndex("dbo.GameCategories", new[] { "Category_Id" });
            DropIndex("dbo.GameCategories", new[] { "Game_Id" });
            DropIndex("dbo.TeamGames", new[] { "Game_Id" });
            DropIndex("dbo.TeamGames", new[] { "Team_Id" });
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Estate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estates", t => t.Estate_Id)
                .Index(t => t.Estate_Id);
            
            CreateTable(
                "dbo.Estates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryGames",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Game_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.GameTeams",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.Team_Id })
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.Team_Id);
            
            AddColumn("dbo.Addresses", "City_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "City_Id");
            AddForeignKey("dbo.Addresses", "City_Id", "dbo.Cities", "Id", cascadeDelete: true);
            DropColumn("dbo.Addresses", "Cidade");
            DropTable("dbo.GameCategories");
            DropTable("dbo.TeamGames");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Game_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Category_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Addresses", "Cidade", c => c.String(nullable: false));
            DropForeignKey("dbo.Cities", "Estate_Id", "dbo.Estates");
            DropForeignKey("dbo.GameTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.GameTeams", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.CategoryGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.CategoryGames", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropIndex("dbo.GameTeams", new[] { "Team_Id" });
            DropIndex("dbo.GameTeams", new[] { "Game_Id" });
            DropIndex("dbo.CategoryGames", new[] { "Game_Id" });
            DropIndex("dbo.CategoryGames", new[] { "Category_Id" });
            DropIndex("dbo.Cities", new[] { "Estate_Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropColumn("dbo.Addresses", "City_Id");
            DropTable("dbo.GameTeams");
            DropTable("dbo.CategoryGames");
            DropTable("dbo.Estates");
            DropTable("dbo.Cities");
            CreateIndex("dbo.TeamGames", "Team_Id");
            CreateIndex("dbo.TeamGames", "Game_Id");
            CreateIndex("dbo.GameCategories", "Game_Id");
            CreateIndex("dbo.GameCategories", "Category_Id");
            AddForeignKey("dbo.TeamGames", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameCategories", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameCategories", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
