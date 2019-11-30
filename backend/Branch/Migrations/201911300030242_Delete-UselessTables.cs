namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUselessTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "EstateId", "dbo.Estates");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "EstateId" });
            DropTable("dbo.Addresses");
            DropTable("dbo.Cities");
            DropTable("dbo.Estates");
        }
        
        public override void Down()
        {
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
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        EstateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CEP = c.String(nullable: false),
                        Logradouro = c.String(nullable: false),
                        Complemento = c.String(nullable: false),
                        Bairro = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Cities", "EstateId");
            CreateIndex("dbo.Addresses", "CityId");
            CreateIndex("dbo.Addresses", "UserId");
            AddForeignKey("dbo.Addresses", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Addresses", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cities", "EstateId", "dbo.Estates", "Id", cascadeDelete: true);
        }
    }
}
