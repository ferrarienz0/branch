using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.Models.NoSQL
{
    public class ExampleModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}