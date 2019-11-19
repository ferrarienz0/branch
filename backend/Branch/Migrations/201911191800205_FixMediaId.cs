namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMediaId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "MediaId", "dbo.Media");
            DropIndex("dbo.Subjects", new[] { "MediaId" });
            AlterColumn("dbo.Subjects", "MediaId", c => c.Int());
            CreateIndex("dbo.Subjects", "MediaId");
            AddForeignKey("dbo.Subjects", "MediaId", "dbo.Media", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "MediaId", "dbo.Media");
            DropIndex("dbo.Subjects", new[] { "MediaId" });
            AlterColumn("dbo.Subjects", "MediaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "MediaId");
            AddForeignKey("dbo.Subjects", "MediaId", "dbo.Media", "Id", cascadeDelete: true);
        }
    }
}
