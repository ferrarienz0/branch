namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "MediaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "MediaId");
            AddForeignKey("dbo.Subjects", "MediaId", "dbo.Media", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "MediaId", "dbo.Media");
            DropIndex("dbo.Subjects", new[] { "MediaId" });
            DropColumn("dbo.Subjects", "MediaId");
        }
    }
}
