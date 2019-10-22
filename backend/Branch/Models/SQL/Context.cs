using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Pro> Pros { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Media> Medias { get; set; }

        public DbSet<TypeMedia> TypeMedias { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Marketplace> Marketplaces { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<TypeProduct> TypeProducts { get; set; }

        public DbSet<GameMedia> GameMedias { get; set; }

        public DbSet<MarketplaceMedia> MarketplaceMedias { get; set; }

        public DbSet<ProductCart> ProductCarts { get; set; }

        public DbSet<ProductMedia> ProductMedias { get; set; }

        public DbSet<SubjectGame> SubjectGames { get; set; }

        public DbSet<UserGame> UserGames { get; set; }

        public DbSet<UserMedia> UserMedias { get; set; }

        public DbSet<UserSubject> UserSubjects { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Estate> States { get; set; }

        public Context() : base("branchsql")
        {

        }
    }
}