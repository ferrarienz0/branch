using Branch.JWTProvider;
using Branch.Models;
using Branch.Models.NoSQL;
using Branch.SearchAuxiliars;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Branch.Controllers
{
    public class PostController : ApiController
    {
        private readonly NoSQLContext NoSQLContext = new NoSQLContext();
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpPost]
        [Route("post/create")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult CreatePost([FromBody] Post NewPost, [FromUri] string AccessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = TokenValidator.VerifyToken(AccessToken);
            
            NewPost.UserId = UserId;

            NewPost = PostSearchAuxiliar.UpdateOwner(NewPost);

            NewPost = TreatPostAddons(NewPost);

            NoSQLContext.PostCollection.InsertOne(NewPost);

            if (NewPost.Parent != ObjectId.Empty)
            {
                var Parent = FindParent(NewPost);

                if (Parent != default)
                {
                    UpdateParent(Parent, NewPost.Id);
                }
            }

            return Ok(NewPost);
        }

        [HttpGet]
        [Route("posts")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult RecommendedPosts([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var UserFollows = UserSearchAuxiliar.Follows(UserId);
            var UserTopics = UserSearchAuxiliar.FollowedSubjects(UserId);

            var UserPosts = PostSearchAuxiliar.PostsByAuthor(UserId);
            var UserMentionPosts = PostSearchAuxiliar.MentionsUser(UserId);

            var FollowsPosts = PostSearchAuxiliar.PostsByAuthors(UserFollows.Select(x => x.Id));
            var FollowsMentionPosts = PostSearchAuxiliar.MentionsUsers(UserFollows);

            var TopicsPosts = PostSearchAuxiliar.PostsBySubjects(UserTopics.Select(x => x.Id));

            var PostComparer = new PostComparer();

            var RecommendedPosts = UserPosts
                                             .Union(UserMentionPosts, PostComparer)
                                             .Union(FollowsPosts, PostComparer)
                                             .Union(FollowsMentionPosts, PostComparer)
                                             .Union(TopicsPosts, PostComparer)
                                             .ToList();


            RecommendedPosts = PostSearchAuxiliar.UpdateOwner(RecommendedPosts);

            return Ok(RecommendedPosts);
        }

        [HttpGet]
        [Route("post")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostById([FromUri] string PostId)
        {
            var Post = PostSearchAuxiliar.PostById(PostId);
            Post = PostSearchAuxiliar.UpdateOwner(Post);

            return Ok(Post);
        }

        [HttpGet]
        [Route("posts/subject")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostsBySubject([FromUri] int SubjectId)
        {
            var Posts = PostSearchAuxiliar.PostsBySubject(SubjectId);
            Posts = PostSearchAuxiliar.UpdateOwner(Posts);

            return Ok(Posts);
        }

        [HttpGet]
        [Route("posts/user")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostsByUser([FromUri] int UserId)
        {
            var Posts = PostSearchAuxiliar.PostsByAuthor(UserId);
            Posts = PostSearchAuxiliar.UpdateOwner(Posts);

            return Ok(Posts);
        }

        [HttpGet]
        [Route("post/comments")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostComments([FromUri] string PostId)
        {
            var Comments = PostSearchAuxiliar.PostComments(PostId);
            Comments = PostSearchAuxiliar.UpdateOwner(Comments);

            return Ok(Comments);
        }

        [HttpPut]
        [Route("post/like")]
        public IHttpActionResult Like([FromUri] string AccessToken, [FromUri] string PostId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var PostLiked = PostSearchAuxiliar.PostById(PostId);
         
            if (PostLiked.Likes.Contains(UserId))
            {
                PostLiked.Likes.Remove(UserId);
            }
            else
            {
                PostLiked.Likes.Add(UserId);
            }

            if (PostLiked.Dislikes.Contains(UserId))
            {
                PostLiked.Dislikes.Remove(UserId);
            }

            NoSQLContext.PostCollection.FindOneAndReplace(x => x.Id == PostLiked.Id, PostLiked);

            int TotalLikes = PostLiked.Likes.Count;
            int TotalDeslikes = PostLiked.Dislikes.Count;

            dynamic Response = new { TotalLikes, TotalDeslikes };

            return Ok(Response);
        }

        [HttpPut]
        [Route("post/dislike")]
        [ResponseType(typeof(int))]
        public IHttpActionResult Dislike([FromUri] string AccessToken, [FromUri] string PostId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var PostDisliked = PostSearchAuxiliar.PostById(PostId);

            if (PostDisliked.Dislikes.Contains(UserId))
            {
                PostDisliked.Dislikes.Remove(UserId);
            }
            else
            {
                PostDisliked.Dislikes.Add(UserId);
            }

            if(PostDisliked.Likes.Contains(UserId))
            {
                PostDisliked.Likes.Remove(UserId);
            }

            NoSQLContext.PostCollection.FindOneAndReplace(x => x.Id == PostDisliked.Id, PostDisliked);

            int TotalLikes = PostDisliked.Likes.Count;
            int TotalDeslikes = PostDisliked.Dislikes.Count;

            dynamic Response = new { TotalLikes, TotalDeslikes };

            return Ok(Response);
        }

        private List<string> FindTag(string Text, string Tag)
        {
            int TagIndex;
            List<string> Tags = new List<string>();

            while ((TagIndex = Text.IndexOf(Tag)) != -1)
            {
                var End = Text.IndexOf(" ", TagIndex + 1);

                if(End == - 1)
                {
                    End = Text.Length;
                }

                var TagFound = Text.Substring(TagIndex, End - TagIndex);

                if (TagIndex - 1 < 0)
                {
                    Tags.Add(TagFound);
                }
                else if(Text[TagIndex - 1] == ' ')
                {
                    Tags.Add(TagFound);
                }
                
                Text = Text.Replace(TagFound, "");
            }

            return Tags;
        }

        private List<int> CheckHashtagsExistence(List<string> Hashtags)
        {
            List<int> Subjects = new List<int>();

            foreach (var Hashtag in Hashtags)
            {
                var Exists = SQLContext.Subjects.FirstOrDefault(x => x.Hashtag == Hashtag);

                if (Exists == default)
                {
                    var Added = SQLContext.Subjects.Add(new Subject() { Hashtag = Hashtag });
                    SQLContext.SaveChanges();

                    Subjects.Add(Added.Id);
                }

                else
                {
                    Subjects.Add(Exists.Id);
                }
            }

            return Subjects;
        }

        private List<User> CheckMentionsExistence(List<string> Mentions)
        {
            List<User> UsersList = new List<User>();

            foreach (var Mention in Mentions)
            {
                var Exists = SQLContext.Users.FirstOrDefault(x => x.Nickname == Mention.Substring(1, Mention.Length));

                if (Exists != default)
                {
                    UsersList.Add(Exists);
                }
            }

            return UsersList;
        }

        private List<Product> CheckProductExistence(List<string> Products)
        {
            List<Product> UsersList = new List<Product>();

            foreach (var Product in Products)
            {
                var Exists = SQLContext.Products.FirstOrDefault(x => x.Name == Product.Substring(1, Product.Length));

                if (Exists != default)
                {
                    UsersList.Add(Exists);
                }
            }

            return UsersList;
        }

        private Post TreatPostAddons(Post NewPost)
        {
            var Hashtags = FindTag(NewPost.Text, "#");
            var Mentions = FindTag(NewPost.Text, "@");
            var Products = FindTag(NewPost.Text, "$");

            var HashtagObjects = CheckHashtagsExistence(Hashtags);
            var MentionObjects = CheckMentionsExistence(Mentions);
            var ProductsObjects = CheckProductExistence(Products);

            NewPost.Hashtags = HashtagObjects;
            NewPost.Mentions = MentionObjects;
            NewPost.Products = ProductsObjects;

            var Medias = new List<Media>();

            if(NewPost.Medias != default)
            {
                foreach (var Id in NewPost.Medias)
                {
                    var Media = SQLContext.Medias.Find(Id);
                    Medias.Add(Media);
                }
            }

            NewPost.MediaObjects = Medias;
            NewPost.Likes = new List<int>();
            NewPost.Dislikes = new List<int>();
            NewPost.Comments = new List<ObjectId>();

            return NewPost;
        }

        private Post FindParent(Post NewPost)
        {
            var ParentPost = PostSearchAuxiliar.PostById(NewPost.Parent.ToString());
            
            return ParentPost;
        }

        private void UpdateParent(Post Parent, ObjectId CommentId)
        {
            Parent.Comments.Add(CommentId);
            NoSQLContext.PostCollection.UpdateOne(x => x.Id == Parent.Id,
                                                             Builders<Post>.Update.Set(Post => Post, Parent));
        }
    }
}
