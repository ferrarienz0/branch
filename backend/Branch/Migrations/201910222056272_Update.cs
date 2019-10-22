namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryGames", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GameTeams", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GameTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Estate_Id", "dbo.Estates");
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "Estate_Id" });
            DropIndex("dbo.CategoryGames", new[] { "Category_Id" });
            DropIndex("dbo.CategoryGames", new[] { "Game_Id" });
            DropIndex("dbo.GameTeams", new[] { "Game_Id" });
            DropIndex("dbo.GameTeams", new[] { "Team_Id" });
            RenameColumn(table: "dbo.Addresses", name: "City_Id", newName: "CityId");
            RenameColumn(table: "dbo.Cities", name: "Estate_Id", newName: "EstateId");
            AlterColumn("dbo.Addresses", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cities", "EstateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "CityId");
            CreateIndex("dbo.Cities", "EstateId");
            AddForeignKey("dbo.Addresses", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cities", "EstateId", "dbo.Estates", "Id", cascadeDelete: true);
            DropTable("dbo.CategoryGames");
            DropTable("dbo.GameTeams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GameTeams",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.Team_Id });
            
            CreateTable(
                "dbo.CategoryGames",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Game_Id });
            
            DropForeignKey("dbo.Cities", "EstateId", "dbo.Estates");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropIndex("dbo.Cities", new[] { "EstateId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            AlterColumn("dbo.Cities", "EstateId", c => c.Int());
            AlterColumn("dbo.Addresses", "CityId", c => c.Int());
            RenameColumn(table: "dbo.Cities", name: "EstateId", newName: "Estate_Id");
            RenameColumn(table: "dbo.Addresses", name: "CityId", newName: "City_Id");
            CreateIndex("dbo.GameTeams", "Team_Id");
            CreateIndex("dbo.GameTeams", "Game_Id");
            CreateIndex("dbo.CategoryGames", "Game_Id");
            CreateIndex("dbo.CategoryGames", "Category_Id");
            CreateIndex("dbo.Cities", "Estate_Id");
            CreateIndex("dbo.Addresses", "City_Id");
            AddForeignKey("dbo.Cities", "Estate_Id", "dbo.Estates", "Id");
            AddForeignKey("dbo.Addresses", "City_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.GameTeams", "Team_Id", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GameTeams", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryGames", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
