using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class SQLContext : DbContext
    {
        public DbSet<Media> Medias { get; set; }

        public DbSet<TypeMedia> TypeMedias { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCart> ProductCarts { get; set; }

        public DbSet<UserSubject> UserSubjects { get; set; }

        public SQLContext() : base("branchsql")
        {

        }
    }
}