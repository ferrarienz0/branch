using Branch.Models;
using Branch.Models.NoSQL;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.SearchAuxiliars
{
    public static class PostSearchAuxiliar
    {
        private static readonly SQLContext SQLContext = new SQLContext();
        private static readonly NoSQLContext NoSQLContext = new NoSQLContext();

        /// <summary>
        /// Returns posts that mentions a certain user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<Post> MentionsUser(int UserId)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.Mentions.Select(y => y.Id).Contains(UserId))
                                              .ToList();
        }

        /// <summary>
        /// Returns posts that mentions one or more users
        /// </summary>
        /// <param name="Users">The user collection</param>
        public static List<Post> MentionsUsers(IEnumerable<User> Users)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.Mentions.Intersect(Users).Any())
                                              .ToList();
        }

        /// <summary>
        /// Returns user's posts
        /// </summary>
        /// <param name="UserId">The user's id</param>
        public static List<Post> PostsByAuthor(int UserId)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.UserId == UserId)
                                              .ToList();
        }

        /// <summary>
        /// Returns users' posts
        /// </summary>
        /// <param name="UserIds">The users' ids</param>
        public static List<Post> PostsByAuthors(IEnumerable<int> UserIds)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => UserIds.Contains(x.UserId))
                                              .ToList();
        }

        /// <summary>
        /// Returns the subject's posts
        /// </summary>
        /// <param name="SubjectId">The subject's id</param>
        public static List<Post> PostsBySubject(int SubjectId)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.Hashtags.Contains(SubjectId))
                                              .ToList();
        }

        /// <summary>
        /// Returns topics' posts
        /// </summary>
        /// <param name="SubjectIds">The subjects' ids</param>
        public static List<Post> PostsBySubjects(IEnumerable<int> SubjectIds)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.Hashtags.Intersect(SubjectIds).Any())
                                              .ToList();
        }

        /// <summary>
        /// Returns the product's posts
        /// </summary>
        /// <param name="ProductId">The product's id</param>
        public static List<Post> PostsByProduct(int ProductId)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.Products.Exists(y => y.Id == ProductId))
                                              .ToList();
        }

        /// <summary>
        /// Returns products' posts
        /// </summary>
        /// <param name="ProductIds">The products' ids</param>
        public static List<Post> PostsByProducts(IEnumerable<int> ProductIds)
        {
            return NoSQLContext.PostCollection
                                              .Find(x => x.Products
                                                                   .Select(y => y.Id)
                                                                   .Intersect(ProductIds)
                                                                   .Any())
                                              .ToList();
        }

        /// <summary>
        /// Returns a post with certain id
        /// </summary>
        /// <param name="PostId">The post's id</param>
        public static Post PostById(string PostId)
        {
            var Filter = Builders<Post>.Filter.Eq("Id", ObjectId.Parse(PostId));

            return NoSQLContext.PostCollection
                                              .Find(Filter)
                                              .FirstOrDefault();
        }

        /// <summary>
        /// Returns a post's comments
        /// </summary>
        /// <param name="PostId">The post's id</param>
        public static List<Post> PostComments(string PostId)
        {
            var Post = PostById(PostId);

            var CommentFilter = Builders<Post>.Filter.In(x => x.Id, Post.Comments);

            return NoSQLContext.PostCollection
                                              .Find(CommentFilter)
                                              .ToList();
        }

        /// <summary>
        /// Updates the posts' owner
        /// </summary>
        /// <param name="Posts">The post collection</param>
        public static List<Post> UpdateOwner(List<Post> Posts)
        {
            for(int i = 0; i < Posts.Count; i++)
            {
                Posts[i] = UpdateOwner(Posts[i]);
            }

            return Posts;
        }

        /// <summary>
        /// Updates the post's owner
        /// </summary>
        /// <param name="Post">The post</param>
        public static Post UpdateOwner(Post Post)
        {
            var User = SQLContext.Users.Find(Post.UserId);

            Post.Owner = new Owner()
            {
                Id = User.Id,
                Firstname = User.Firstname,
                Lastname = User.Lastname,
                Nickname = User.Nickname,
                MediaURL = User.Media.URL
            };

            return Post;
        }

    }
}