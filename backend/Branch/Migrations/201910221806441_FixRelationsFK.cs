namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelationsFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Addresses", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Addresses", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Addresses", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Addresses", name: "UserId", newName: "User_Id");
        }
    }
}
