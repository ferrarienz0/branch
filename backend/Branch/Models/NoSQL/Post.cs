using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.Models.NoSQL
{
    public class Post
    {
        [BsonId][BsonRequired()]
        public ObjectId ID { get; set; }

        [BsonElement("user_id")][BsonRequired()]
        public int IDUser { get; set; }

        [BsonElement("content_path")]
        [BsonRequired()]
        public string ContentPath { get; set; }

        [BsonElement("ggs")]
        [BsonRequired()]
        public string GGs { get; set; }

        [BsonElement("bgs")]
        [BsonRequired()]
        public string BGs { get; set; }

        [BsonElement("hfs")]
        [BsonRequired()]
        public string HFs { get; set; }

        [BsonElement("trolls")]
        [BsonRequired()]
        public string Trolls { get; set; }

        [BsonElement("hashtags")]
        [BsonRequired()]
        public List<string> Hashtags { get; set; }

        [BsonElement("mentions")]
        [BsonRequired()]
        public List<int> Mentions { get; set; }

        [BsonElement("comments")]
        [BsonRequired()]
        public List<ObjectId> Comments { get; set; }

        [BsonElement("shares")]
        [BsonRequired()]
        public List<int> Shares { get; set; }
    }
}