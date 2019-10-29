using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

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

        [BsonElement("owner")]
        public virtual User Owner { get; set; }

        [BsonElement("type")]
        [BsonRequired()]
        public string Type { get; set; }

        [BsonElement("text")]
        [BsonRequired()]
        public string Text { get; set; }

        [BsonElement("medias")]
        public List<int> Medias { get; set; }

        [BsonElement("medias_urls")]
        public List<Media> MediaObjects { get; set; }

        [BsonElement("hashtags")]
        public List<Subject> Hashtags { get; set; }

        [BsonElement("mentions")]
        public List<User> Mentions { get; set; }

        [BsonElement("products")]
        public List<Product> Products { get; set; }

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