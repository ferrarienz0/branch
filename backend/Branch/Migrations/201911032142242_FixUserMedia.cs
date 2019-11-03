namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserMedia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMedias", "MediaId", "dbo.Media");
            DropForeignKey("dbo.UserMedias", "TypeMediaId", "dbo.TypeMedias");
            DropForeignKey("dbo.UserMedias", "UserId", "dbo.Users");
            DropIndex("dbo.UserMedias", new[] { "MediaId" });
            DropIndex("dbo.UserMedias", new[] { "TypeMediaId" });
            DropIndex("dbo.UserMedias", new[] { "UserId" });
            AddColumn("dbo.Users", "MediaId", c => c.Int());
            CreateIndex("dbo.Users", "MediaId");
            AddForeignKey("dbo.Users", "MediaId", "dbo.Media", "Id");
            DropTable("dbo.UserMedias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaId = c.Int(nullable: false),
                        TypeMediaId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Users", "MediaId", "dbo.Media");
            DropIndex("dbo.Users", new[] { "MediaId" });
            DropColumn("dbo.Users", "MediaId");
            CreateIndex("dbo.UserMedias", "UserId");
            CreateIndex("dbo.UserMedias", "TypeMediaId");
            CreateIndex("dbo.UserMedias", "MediaId");
            AddForeignKey("dbo.UserMedias", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserMedias", "TypeMediaId", "dbo.TypeMedias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserMedias", "MediaId", "dbo.Media", "Id", cascadeDelete: true);
        }
    }
}
