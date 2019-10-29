namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFollow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Follows", "Followed_Id", "dbo.Users");
            DropIndex("dbo.Follows", new[] { "Followed_Id" });
            RenameColumn(table: "dbo.Follows", name: "Followed_Id", newName: "FollowedId");
            AlterColumn("dbo.Follows", "FollowedId", c => c.Int(nullable: false));
            CreateIndex("dbo.Follows", "FollowedId");
            AddForeignKey("dbo.Follows", "FollowedId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "FollowedId", "dbo.Users");
            DropIndex("dbo.Follows", new[] { "FollowedId" });
            AlterColumn("dbo.Follows", "FollowedId", c => c.Int());
            RenameColumn(table: "dbo.Follows", name: "FollowedId", newName: "Followed_Id");
            CreateIndex("dbo.Follows", "Followed_Id");
            AddForeignKey("dbo.Follows", "Followed_Id", "dbo.Users", "Id");
        }
    }
}
