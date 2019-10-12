namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamMedias", "IDMedia_ID", "dbo.Media");
            DropForeignKey("dbo.TeamMedias", "IDTeam_ID", "dbo.Teams");
            DropForeignKey("dbo.TeamMedias", "IDTypeMedia_ID", "dbo.TypeMedias");
            DropIndex("dbo.TeamMedias", new[] { "IDMedia_ID" });
            DropIndex("dbo.TeamMedias", new[] { "IDTeam_ID" });
            DropIndex("dbo.TeamMedias", new[] { "IDTypeMedia_ID" });
            AddColumn("dbo.Addresses", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Addresses", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Carts", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Carts", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Marketplaces", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Marketplaces", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Pros", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Pros", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Teams", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Teams", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Categories", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Categories", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Follows", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Follows", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.GameCategories", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.GameCategories", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Games", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Games", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.GameMedias", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.GameMedias", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Media", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Media", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.TypeMedias", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.TypeMedias", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.MarketplaceMedias", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.MarketplaceMedias", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.ProductCarts", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.ProductCarts", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Products", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Products", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.TypeProducts", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.TypeProducts", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.ProductMedias", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.ProductMedias", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.SubjectGames", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.SubjectGames", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Subjects", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.Subjects", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.TeamGames", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.TeamGames", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.UserGames", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.UserGames", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.UserMedias", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.UserMedias", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.UserSubjects", "CreatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            AddColumn("dbo.UserSubjects", "UpdatedAt", c => c.DateTime(defaultValueSql: "current_timestamp"));
            DropTable("dbo.TeamMedias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamMedias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMedia_ID = c.Int(nullable: false),
                        IDTeam_ID = c.Int(nullable: false),
                        IDTypeMedia_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.UserSubjects", "UpdatedAt");
            DropColumn("dbo.UserSubjects", "CreatedAt");
            DropColumn("dbo.UserMedias", "UpdatedAt");
            DropColumn("dbo.UserMedias", "CreatedAt");
            DropColumn("dbo.UserGames", "UpdatedAt");
            DropColumn("dbo.UserGames", "CreatedAt");
            DropColumn("dbo.TeamGames", "UpdatedAt");
            DropColumn("dbo.TeamGames", "CreatedAt");
            DropColumn("dbo.Subjects", "UpdatedAt");
            DropColumn("dbo.Subjects", "CreatedAt");
            DropColumn("dbo.SubjectGames", "UpdatedAt");
            DropColumn("dbo.SubjectGames", "CreatedAt");
            DropColumn("dbo.ProductMedias", "UpdatedAt");
            DropColumn("dbo.ProductMedias", "CreatedAt");
            DropColumn("dbo.TypeProducts", "UpdatedAt");
            DropColumn("dbo.TypeProducts", "CreatedAt");
            DropColumn("dbo.Products", "UpdatedAt");
            DropColumn("dbo.Products", "CreatedAt");
            DropColumn("dbo.ProductCarts", "UpdatedAt");
            DropColumn("dbo.ProductCarts", "CreatedAt");
            DropColumn("dbo.MarketplaceMedias", "UpdatedAt");
            DropColumn("dbo.MarketplaceMedias", "CreatedAt");
            DropColumn("dbo.TypeMedias", "UpdatedAt");
            DropColumn("dbo.TypeMedias", "CreatedAt");
            DropColumn("dbo.Media", "UpdatedAt");
            DropColumn("dbo.Media", "CreatedAt");
            DropColumn("dbo.GameMedias", "UpdatedAt");
            DropColumn("dbo.GameMedias", "CreatedAt");
            DropColumn("dbo.Games", "UpdatedAt");
            DropColumn("dbo.Games", "CreatedAt");
            DropColumn("dbo.GameCategories", "UpdatedAt");
            DropColumn("dbo.GameCategories", "CreatedAt");
            DropColumn("dbo.Follows", "UpdatedAt");
            DropColumn("dbo.Follows", "CreatedAt");
            DropColumn("dbo.Categories", "UpdatedAt");
            DropColumn("dbo.Categories", "CreatedAt");
            DropColumn("dbo.Teams", "UpdatedAt");
            DropColumn("dbo.Teams", "CreatedAt");
            DropColumn("dbo.Pros", "UpdatedAt");
            DropColumn("dbo.Pros", "CreatedAt");
            DropColumn("dbo.Marketplaces", "UpdatedAt");
            DropColumn("dbo.Marketplaces", "CreatedAt");
            DropColumn("dbo.Carts", "UpdatedAt");
            DropColumn("dbo.Carts", "CreatedAt");
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Addresses", "UpdatedAt");
            DropColumn("dbo.Addresses", "CreatedAt");
            CreateIndex("dbo.TeamMedias", "IDTypeMedia_ID");
            CreateIndex("dbo.TeamMedias", "IDTeam_ID");
            CreateIndex("dbo.TeamMedias", "IDMedia_ID");
            AddForeignKey("dbo.TeamMedias", "IDTypeMedia_ID", "dbo.TypeMedias", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TeamMedias", "IDTeam_ID", "dbo.Teams", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TeamMedias", "IDMedia_ID", "dbo.Media", "ID", cascadeDelete: true);
        }
    }
}
