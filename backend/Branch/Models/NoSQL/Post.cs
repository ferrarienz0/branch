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

        [BsonElement("is_product")]
        [BsonRequired()]
        public bool IsProduct { get; set; }

        [BsonElement("text")]
        [BsonRequired()]
        public string Text { get; set; }

        [BsonElement("medias")]
        public List<int> Medias { get; set; }

        [BsonElement("hashtags")]
        [BsonRequired()]
        public List<string> Hashtags { get; set; }

        [BsonElement("mentions")]
        [BsonRequired()]
        public List<int> Mentions { get; set; }

        [BsonElement("comments")]
        [BsonRequired()]
        public List<ObjectId> Comments { get; set; }

        [BsonElement("parent")]
        public ObjectId Parent { get; set; }

        [BsonElement("childrens")]
        public List<ObjectId> Childrens { get; set; }
    }
}