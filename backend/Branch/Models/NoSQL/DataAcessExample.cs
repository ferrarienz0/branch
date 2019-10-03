using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.Models.NoSQL
{
    public class DataAcessExample
    {
        MongoClient Client;
        IMongoDatabase DataBase;

        public DataAcessExample()
        {
            Client = new MongoClient("CONNECTION_STRING");
            DataBase = Client.GetDatabase("DATABASE_NAME");
        }
    }
}