using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.Models.NoSQL
{
    public class DataAccess
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }
        public IMongoCollection<Post> PostCollection { get; set; }

        public DataAccess()
        {
            Client = new MongoClient("mongodb+srv://branchadmin:branch6969@branch-nosql-o5f6b.mongodb.net/test?retryWrites=true&w=majority");
            Database = Client.GetDatabase("branch-nosql");
            PostCollection = Database.GetCollection<Post>("posts");
        }
    }
}