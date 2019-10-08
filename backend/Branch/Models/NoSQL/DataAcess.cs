using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.Models.NoSQL
{
    public class DataAcess
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<Post> PostCollection { get; set; }

        public DataAcess()
        {
            Client = new MongoClient("CONNECTION_STRING");
            Database = Client.GetDatabase("DATABASE_NAME");
            PostCollection = Database.GetCollection<Post>("posts");
        }
    }
}