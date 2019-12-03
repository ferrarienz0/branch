using Branch.Auxiliars;
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

            NewPost = PostAuxiliar.UpdateOwner(NewPost, SQLContext);

            NewPost = TreatPostAddons(NewPost);

            NoSQLContext.PostCollection.InsertOne(NewPost);

            Post Parent = default;

            if (NewPost.Parent != null)
            {
                Parent = FindParent(NewPost);

                if (Parent != default)
                {
                    UpdateParent(Parent, NewPost.Id);
                }
            }

            GraphAuxiliar.IncreaseFollowAffinity(NewPost.UserId, NewPost.Mentions, SQLContext);
            GraphAuxiliar.IncreaseTopicAffinity(NewPost.UserId, NewPost.Hashtags, SQLContext);

            if (Parent != default)
            {
                GraphAuxiliar.IncreaseFollowAffinity(NewPost.UserId, Parent.UserId, SQLContext);
                GraphAuxiliar.IncreaseFollowAffinity(NewPost.UserId, Parent.Mentions, SQLContext);
                GraphAuxiliar.IncreaseTopicAffinity(NewPost.UserId, Parent.Hashtags, SQLContext);
            }

            return Ok(NewPost);
        }

        [HttpGet]
        [Route("posts")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult RecommendedPosts([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var UserFollows = UserAuxiliar
                                          .Follows(UserId, SQLContext)
                                          .Select(x => x.Id);

            var UserTopics = UserAuxiliar.FollowedSubjects(UserId, SQLContext);

            var UserPosts = PostAuxiliar.PostsByAuthor(UserId);
            var UserMentionPosts = PostAuxiliar.MentionsUser(UserId, SQLContext);

            var FollowsPosts = PostAuxiliar.PostsByAuthors(UserFollows);
            var FollowsMentionPosts = PostAuxiliar.MentionsUsers(UserFollows);

            var TopicsPosts = PostAuxiliar.PostsBySubjects(UserTopics.Select(x => x.Id));

            var PostComparer = new PostComparer();

            var RecommendedPosts = UserPosts
                                             .Union(UserMentionPosts, PostComparer)
                                             .Union(FollowsPosts, PostComparer)
                                             .Union(FollowsMentionPosts, PostComparer)
                                             .Union(TopicsPosts, PostComparer)
                                             .ToList();

            RecommendedPosts = PostAuxiliar.UpdateOwner(RecommendedPosts, SQLContext);
            RecommendedPosts = GraphAuxiliar.OrderPostsByAffinity(UserId, RecommendedPosts, SQLContext);

            return Ok(RecommendedPosts);
        }

        [HttpGet]
        [Route("post")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostById([FromUri] string PostId)
        {
            var Post = PostAuxiliar.PostById(PostId);
            Post = PostAuxiliar.UpdateOwner(Post, SQLContext);

            return Ok(Post);
        }

        [HttpGet]
        [Route("posts/subject")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostsBySubject([FromUri] int SubjectId)
        {
            var Posts = PostAuxiliar.PostsBySubject(SubjectId);
            Posts = PostAuxiliar.UpdateOwner(Posts, SQLContext);

            return Ok(Posts);
        }

        [HttpGet]
        [Route("posts/user")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostsByUser([FromUri] int UserId)
        {
            var Posts = PostAuxiliar.PostsByAuthor(UserId);
            Posts = PostAuxiliar.UpdateOwner(Posts, SQLContext);

            var MentionsPosts = PostAuxiliar.MentionsUser(UserId, SQLContext);

            var Response = Posts.Union(MentionsPosts, new PostComparer()).ToList();

            return Ok(Response);
        }

        [HttpGet]
        [Route("posts/product")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostsByProduct([FromUri] int ProductId)
        {
            var Posts = PostAuxiliar.PostsByProduct(ProductId);
            Posts = PostAuxiliar.UpdateOwner(Posts, SQLContext);

            return Ok(Posts);
        }

        [HttpGet]
        [Route("post/comments")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult PostComments([FromUri] string PostId)
        {
            var Comments = PostAuxiliar.PostComments(PostId);
            Comments = PostAuxiliar.UpdateOwner(Comments, SQLContext);

            return Ok(Comments);
        }

        [HttpPut]
        [Route("post/like")]
        public IHttpActionResult Like([FromUri] string AccessToken, [FromUri] string PostId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var PostLiked = PostAuxiliar.PostById(PostId);
         
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

            var Update = Builders<Post>.Update
                                               .Set("Likes", PostLiked.Likes)
                                               .Set("Dislikes", PostLiked.Dislikes);

            PostAuxiliar.UpdatePostById(PostLiked.Id, Update);

            int TotalLikes = PostLiked.Likes.Count;
            int TotalDeslikes = PostLiked.Dislikes.Count;

            dynamic Response = new { TotalLikes, TotalDeslikes };

            GraphAuxiliar.IncreaseFollowAffinity(UserId, PostLiked.UserId, SQLContext);
            GraphAuxiliar.IncreaseTopicAffinity(UserId, PostLiked.Hashtags, SQLContext);

            return Ok(Response);
        }

        [HttpPut]
        [Route("post/dislike")]
        [ResponseType(typeof(int))]
        public IHttpActionResult Dislike([FromUri] string AccessToken, [FromUri] string PostId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var PostDisliked = PostAuxiliar.PostById(PostId);

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

            var Update = Builders<Post>.Update
                                               .Set("Likes", PostDisliked.Likes)
                                               .Set("Dislikes", PostDisliked.Dislikes);

            PostAuxiliar.UpdatePostById(PostDisliked.Id, Update);

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

        private List<int> CheckHashtagsExistence(List<string> Hashtags, Post NewPost)
        {
            List<int> Subjects = new List<int>();

            foreach (var Hashtag in Hashtags)
            {
                var Exists = SQLContext.Subjects.FirstOrDefault(x => x.Hashtag == Hashtag);

                if (Exists == default)
                {
                    var NewSubject = new Subject()
                    {
                        Hashtag = Hashtag
                    };

                    if(NewPost.Medias != null)
                    {
                        if(NewPost.Medias.Count > 0)
                        {
                            NewSubject.MediaId = NewPost.Medias[0];
                        }
                    }

                    var Added = SQLContext.Subjects.Add(NewSubject);
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

        private List<int> CheckMentionsExistence(List<string> Mentions)
        {
            var UsersList = new List<int>();

            foreach (var Mention in Mentions)
            {
                var User = SQLContext.Users
                                            .FirstOrDefault(x => x.Nickname == Mention.Substring(1, Mention.Length));

                if (User != default)
                {
                    UsersList.Add(User.Id);
                }
            }

            return UsersList;
        }

        private List<int> CheckProductExistence(List<string> Products)
        {
            var ProductList = new List<int>();

            foreach (var Product in Products)
            {
                var _Product = SQLContext.Products.FirstOrDefault(x => x.Name == Product.Substring(1, Product.Length));

                if (_Product != default)
                {
                    ProductList.Add(_Product.Id);
                }
            }

            return ProductList;
        }

        private Post TreatPostAddons(Post NewPost)
        {
            var Hashtags = FindTag(NewPost.Text, "#");
            var Mentions = FindTag(NewPost.Text, "@");
            var Products = FindTag(NewPost.Text, "$");

            var HashtagObjects = CheckHashtagsExistence(Hashtags, NewPost);
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
            var ParentPost = PostAuxiliar.PostById(NewPost.Parent);
            
            return ParentPost;
        }

        private void UpdateParent(Post Parent, ObjectId CommentId)
        {
            var NewComments = new List<ObjectId>(Parent.Comments)
            {
                CommentId
            };

            var Update = Builders<Post>.Update.Set("Comments", NewComments);
            PostAuxiliar.UpdatePostById(Parent.Id, Update);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SQLContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
