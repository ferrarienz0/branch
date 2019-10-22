namespace Branch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixAll1_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Follows", "Followed_Id", "dbo.Users");
            DropIndex("dbo.Follows", new[] { "Followed_Id" });
            RenameColumn(table: "dbo.Carts", name: "Marketplace_Id", newName: "MarketplaceId");
            RenameColumn(table: "dbo.Carts", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Marketplaces", name: "Pro_Id", newName: "ProId");
            RenameColumn(table: "dbo.Pros", name: "Team_Id", newName: "TeamId");
            RenameColumn(table: "dbo.Pros", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Follows", name: "Follower_Id", newName: "FollowerId");
            RenameColumn(table: "dbo.GameMedias", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.GameMedias", name: "Media_Id", newName: "MediaId");
            RenameColumn(table: "dbo.GameMedias", name: "TypeMedia_Id", newName: "TypeMediaId");
            RenameColumn(table: "dbo.MarketplaceMedias", name: "Marketplace_Id", newName: "MarketplaceId");
            RenameColumn(table: "dbo.MarketplaceMedias", name: "Media_Id", newName: "MediaId");
            RenameColumn(table: "dbo.MarketplaceMedias", name: "TypeMedia_Id", newName: "TypeMediaId");
            RenameColumn(table: "dbo.ProductCarts", name: "Cart_Id", newName: "CartId");
            RenameColumn(table: "dbo.ProductCarts", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.Products", name: "Marketplace_Id", newName: "MarketplaceId");
            RenameColumn(table: "dbo.Products", name: "TypeProduct_Id", newName: "TypeProductId");
            RenameColumn(table: "dbo.ProductMedias", name: "Media_Id", newName: "MediaId");
            RenameColumn(table: "dbo.ProductMedias", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "dbo.ProductMedias", name: "TypeMedia_Id", newName: "TypeMediaId");
            RenameColumn(table: "dbo.SubjectGames", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.SubjectGames", name: "Subject_Id", newName: "SubjectId");
            RenameColumn(table: "dbo.UserGames", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.UserGames", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.UserMedias", name: "Media_Id", newName: "MediaId");
            RenameColumn(table: "dbo.UserMedias", name: "TypeMedia_Id", newName: "TypeMediaId");
            RenameColumn(table: "dbo.UserMedias", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.UserSubjects", name: "Subject_Id", newName: "SubjectId");
            RenameColumn(table: "dbo.UserSubjects", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Carts", name: "IX_Marketplace_Id", newName: "IX_MarketplaceId");
            RenameIndex(table: "dbo.Carts", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Marketplaces", name: "IX_Pro_Id", newName: "IX_ProId");
            RenameIndex(table: "dbo.Pros", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Pros", name: "IX_Team_Id", newName: "IX_TeamId");
            RenameIndex(table: "dbo.Follows", name: "IX_Follower_Id", newName: "IX_FollowerId");
            RenameIndex(table: "dbo.GameMedias", name: "IX_Media_Id", newName: "IX_MediaId");
            RenameIndex(table: "dbo.GameMedias", name: "IX_TypeMedia_Id", newName: "IX_TypeMediaId");
            RenameIndex(table: "dbo.GameMedias", name: "IX_Game_Id", newName: "IX_GameId");
            RenameIndex(table: "dbo.MarketplaceMedias", name: "IX_Media_Id", newName: "IX_MediaId");
            RenameIndex(table: "dbo.MarketplaceMedias", name: "IX_TypeMedia_Id", newName: "IX_TypeMediaId");
            RenameIndex(table: "dbo.MarketplaceMedias", name: "IX_Marketplace_Id", newName: "IX_MarketplaceId");
            RenameIndex(table: "dbo.ProductCarts", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.ProductCarts", name: "IX_Cart_Id", newName: "IX_CartId");
            RenameIndex(table: "dbo.Products", name: "IX_TypeProduct_Id", newName: "IX_TypeProductId");
            RenameIndex(table: "dbo.Products", name: "IX_Marketplace_Id", newName: "IX_MarketplaceId");
            RenameIndex(table: "dbo.ProductMedias", name: "IX_Media_Id", newName: "IX_MediaId");
            RenameIndex(table: "dbo.ProductMedias", name: "IX_TypeMedia_Id", newName: "IX_TypeMediaId");
            RenameIndex(table: "dbo.ProductMedias", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.SubjectGames", name: "IX_Subject_Id", newName: "IX_SubjectId");
            RenameIndex(table: "dbo.SubjectGames", name: "IX_Game_Id", newName: "IX_GameId");
            RenameIndex(table: "dbo.UserGames", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserGames", name: "IX_Game_Id", newName: "IX_GameId");
            RenameIndex(table: "dbo.UserMedias", name: "IX_Media_Id", newName: "IX_MediaId");
            RenameIndex(table: "dbo.UserMedias", name: "IX_TypeMedia_Id", newName: "IX_TypeMediaId");
            RenameIndex(table: "dbo.UserMedias", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserSubjects", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserSubjects", name: "IX_Subject_Id", newName: "IX_SubjectId");
            AlterColumn("dbo.Follows", "Followed_Id", c => c.Int());
            CreateIndex("dbo.Follows", "Followed_Id");
            AddForeignKey("dbo.Follows", "Followed_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "Followed_Id", "dbo.Users");
            DropIndex("dbo.Follows", new[] { "Followed_Id" });
            AlterColumn("dbo.Follows", "Followed_Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.UserSubjects", name: "IX_SubjectId", newName: "IX_Subject_Id");
            RenameIndex(table: "dbo.UserSubjects", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.UserMedias", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.UserMedias", name: "IX_TypeMediaId", newName: "IX_TypeMedia_Id");
            RenameIndex(table: "dbo.UserMedias", name: "IX_MediaId", newName: "IX_Media_Id");
            RenameIndex(table: "dbo.UserGames", name: "IX_GameId", newName: "IX_Game_Id");
            RenameIndex(table: "dbo.UserGames", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.SubjectGames", name: "IX_GameId", newName: "IX_Game_Id");
            RenameIndex(table: "dbo.SubjectGames", name: "IX_SubjectId", newName: "IX_Subject_Id");
            RenameIndex(table: "dbo.ProductMedias", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.ProductMedias", name: "IX_TypeMediaId", newName: "IX_TypeMedia_Id");
            RenameIndex(table: "dbo.ProductMedias", name: "IX_MediaId", newName: "IX_Media_Id");
            RenameIndex(table: "dbo.Products", name: "IX_MarketplaceId", newName: "IX_Marketplace_Id");
            RenameIndex(table: "dbo.Products", name: "IX_TypeProductId", newName: "IX_TypeProduct_Id");
            RenameIndex(table: "dbo.ProductCarts", name: "IX_CartId", newName: "IX_Cart_Id");
            RenameIndex(table: "dbo.ProductCarts", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "dbo.MarketplaceMedias", name: "IX_MarketplaceId", newName: "IX_Marketplace_Id");
            RenameIndex(table: "dbo.MarketplaceMedias", name: "IX_TypeMediaId", newName: "IX_TypeMedia_Id");
            RenameIndex(table: "dbo.MarketplaceMedias", name: "IX_MediaId", newName: "IX_Media_Id");
            RenameIndex(table: "dbo.GameMedias", name: "IX_GameId", newName: "IX_Game_Id");
            RenameIndex(table: "dbo.GameMedias", name: "IX_TypeMediaId", newName: "IX_TypeMedia_Id");
            RenameIndex(table: "dbo.GameMedias", name: "IX_MediaId", newName: "IX_Media_Id");
            RenameIndex(table: "dbo.Follows", name: "IX_FollowerId", newName: "IX_Follower_Id");
            RenameIndex(table: "dbo.Pros", name: "IX_TeamId", newName: "IX_Team_Id");
            RenameIndex(table: "dbo.Pros", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Marketplaces", name: "IX_ProId", newName: "IX_Pro_Id");
            RenameIndex(table: "dbo.Carts", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Carts", name: "IX_MarketplaceId", newName: "IX_Marketplace_Id");
            RenameColumn(table: "dbo.UserSubjects", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.UserSubjects", name: "SubjectId", newName: "Subject_Id");
            RenameColumn(table: "dbo.UserMedias", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.UserMedias", name: "TypeMediaId", newName: "TypeMedia_Id");
            RenameColumn(table: "dbo.UserMedias", name: "MediaId", newName: "Media_Id");
            RenameColumn(table: "dbo.UserGames", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.UserGames", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.SubjectGames", name: "SubjectId", newName: "Subject_Id");
            RenameColumn(table: "dbo.SubjectGames", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.ProductMedias", name: "TypeMediaId", newName: "TypeMedia_Id");
            RenameColumn(table: "dbo.ProductMedias", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.ProductMedias", name: "MediaId", newName: "Media_Id");
            RenameColumn(table: "dbo.Products", name: "TypeProductId", newName: "TypeProduct_Id");
            RenameColumn(table: "dbo.Products", name: "MarketplaceId", newName: "Marketplace_Id");
            RenameColumn(table: "dbo.ProductCarts", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.ProductCarts", name: "CartId", newName: "Cart_Id");
            RenameColumn(table: "dbo.MarketplaceMedias", name: "TypeMediaId", newName: "TypeMedia_Id");
            RenameColumn(table: "dbo.MarketplaceMedias", name: "MediaId", newName: "Media_Id");
            RenameColumn(table: "dbo.MarketplaceMedias", name: "MarketplaceId", newName: "Marketplace_Id");
            RenameColumn(table: "dbo.GameMedias", name: "TypeMediaId", newName: "TypeMedia_Id");
            RenameColumn(table: "dbo.GameMedias", name: "MediaId", newName: "Media_Id");
            RenameColumn(table: "dbo.GameMedias", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.Follows", name: "FollowerId", newName: "Follower_Id");
            RenameColumn(table: "dbo.Pros", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Pros", name: "TeamId", newName: "Team_Id");
            RenameColumn(table: "dbo.Marketplaces", name: "ProId", newName: "Pro_Id");
            RenameColumn(table: "dbo.Carts", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Carts", name: "MarketplaceId", newName: "Marketplace_Id");
            CreateIndex("dbo.Follows", "Followed_Id");
            AddForeignKey("dbo.Follows", "Followed_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
