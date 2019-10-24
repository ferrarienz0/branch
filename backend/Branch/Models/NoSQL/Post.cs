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
        [BsonId]
        [BsonElement("id")]
        public ObjectId Id { get; set; }

        [BsonElement("user_id")]
        [BsonRequired()]
        public int UserId { get; set; }

        [BsonElement("type")]
        [BsonRequired()]
        public string Type { get; set; }

        [BsonElement("text")]
        [BsonRequired()]
        public string Text { get; set; }

        [BsonElement("medias")]
        public List<int> Medias { get; set; }

        [BsonElement("hashtags")]
        public List<int> Hashtags { get; set; }

        [BsonElement("mentions")]
        public List<int> Mentions { get; set; }

        [BsonElement("products")]
        public List<int> Products { get; set; }

        [BsonElement("likes")]
        public List<int> Likes { get; set; }

        [BsonElement("dislikes")]
        public List<int> Dislikes { get; set; }

        [BsonElement("comments")]
        public List<ObjectId> Comments { get; set; }

        [BsonElement("parent")]
        public ObjectId Parent { get; set; }
    }
}