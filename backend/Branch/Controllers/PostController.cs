using Branch.JWTProvider;
using Branch.Models;
using Branch.Models.NoSQL;
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
        private readonly DataAcess MongoContext = new DataAcess();
        private readonly Context DB = new Context();

        //TODO: Get Products!!

        [HttpPost]
        [Route("post")]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost([FromBody] Post NewPost, [FromUri] string AccessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = TokenValidator.VerifyToken(AccessToken);
            NewPost.UserId = UserId;

            var Owner = DB.Users.Find(UserId);
            NewPost.Owner = Owner;

            NewPost = await TreatPostAddons(NewPost);

            await MongoContext.PostCollection.InsertOneAsync(NewPost);

            if (NewPost.Parent != ObjectId.Empty)
            {
                var Parent = await FindParent(NewPost);

                if (Parent != default)
                {
                    await UpdateParent(Parent, NewPost.Id);
                }
            }

            return Ok(NewPost);
        }

        [HttpGet]
        [Route("posts")]
        [ResponseType(typeof(List<Post>))]
        public async Task<IHttpActionResult> GetPosts([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var UserFollowings = DB.Follows.Where(x => x.FollowerId == UserId).Select(x => x.Id);
            var FollowingsPosts = MongoContext.PostCollection.Find(x => UserFollowings.Contains(x.UserId)).ToList();

            var UserInterests = DB.UserSubjects.Where(x => x.UserId == UserId).Select(x => x.Id);
            var InterestsPosts = MongoContext.PostCollection.Find(x => x.Hashtags.Exists(y => UserInterests.Contains(y.Id))).ToList();

            var MentionsPosts = MongoContext.PostCollection.Find(x => x.Mentions.Exists(y => y.Id == UserId)).ToList();

            var UserPosts = MongoContext.PostCollection.Find(x => x.UserId == UserId).ToList();
            var UserPostsComments = new List<Post>();

            foreach (Post Post in UserPosts)
            {
                var Comments = await MongoContext.PostCollection.FindAsync(x => x.Parent == Post.Id);
                UserPostsComments = UserPostsComments.Union(Comments.ToList()).ToList();
            }

            var RecommendedPosts = FollowingsPosts.Union(InterestsPosts)
                                                  .Union(MentionsPosts)
                                                  .Union(UserPosts)
                                                  .Union(UserPostsComments).ToList();

            return Ok(RecommendedPosts);
        }

        [HttpPut]
        [Route("posts/like")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Like([FromUri] string AccessToken, ObjectId PostId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var PostLiked = MongoContext.PostCollection.Find(x => x.Id == PostId).FirstOrDefault();
            PostLiked.Likes.Add(UserId);

            int TotalLikes = PostLiked.Likes.Count;

            await MongoContext.PostCollection.UpdateOneAsync(x => x.Id == PostId,
                                                             Builders<Post>.Update.Set(Post => Post, PostLiked));

            return Ok(TotalLikes);
        }

        [HttpPut]
        [Route("posts/dislike")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Dislike([FromUri] string AccessToken, ObjectId PostId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var PostLiked = MongoContext.PostCollection.Find(x => x.Id == PostId).FirstOrDefault();
            PostLiked.Dislikes.Add(UserId);

            int TotalDislikes = PostLiked.Dislikes.Count;

            await MongoContext.PostCollection.UpdateOneAsync(x => x.Id == PostId,
                                                             Builders<Post>.Update.Set(Post => Post, PostLiked));

            return Ok(TotalDislikes);
        }


        // Treatment Functions
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

        private async Task<List<Subject>> CheckHashtagsExistence(List<string> Hashtags)
        {
            List<Subject> Subjects = new List<Subject>();

            foreach (var Hashtag in Hashtags)
            {
                var Exists = await DB.Subjects.FirstOrDefaultAsync(x => x.Hashtag == Hashtag);

                if (Exists == default)
                {
                    var Added = DB.Subjects.Add(new Subject() { Hashtag = Hashtag });
                    Subjects.Add(Added);
                }

                else
                {
                    Subjects.Add(Exists);
                }
            }

            return Subjects;
        }

        private async Task<List<User>> CheckMentionsExistence(List<string> Mentions)
        {
            List<User> UsersList = new List<User>();

            foreach (var Mention in Mentions)
            {
                var Exists = await DB.Users.FirstOrDefaultAsync(x => x.Nickname == Mention.Substring(1, Mention.Length));

                if (Exists != default)
                {
                    UsersList.Add(Exists);
                }
            }

            return UsersList;
        }

        private async Task<List<Product>> CheckProductExistence(List<string> Products)
        {
            List<Product> UsersList = new List<Product>();

            foreach (var Product in Products)
            {
                var Exists = await DB.Products.FirstOrDefaultAsync(x => x.Name == Product.Substring(1, Product.Length));

                if (Exists != default)
                {
                    UsersList.Add(Exists);
                }
            }

            return UsersList;
        }

        private async Task<Post> TreatPostAddons(Post NewPost)
        {
            var Hashtags = FindTag(NewPost.Text, "#");
            var Mentions = FindTag(NewPost.Text, "@");
            var Products = FindTag(NewPost.Text, "$");

            var HashtagObjects = await CheckHashtagsExistence(Hashtags);
            var MentionObjects = await CheckMentionsExistence(Mentions);
            var ProductsObjects = await CheckProductExistence(Products);

            NewPost.Hashtags = HashtagObjects;
            NewPost.Mentions = MentionObjects;
            NewPost.Products = ProductsObjects;

            return NewPost;
        }

        private async Task<Post> FindParent(Post NewPost)
        {
            var ParentPost = await MongoContext.PostCollection.FindAsync(x => x.Id == NewPost.Parent);
            var Result = await ParentPost.FirstOrDefaultAsync();
            
            return Result;
        }

        private async Task UpdateParent(Post Parent, ObjectId CommentId)
        {
            Parent.Comments.Add(CommentId);
            await MongoContext.PostCollection.UpdateOneAsync(x => x.Id == Parent.Id,
                                                             Builders<Post>.Update.Set(Post => Post, Parent));
        }
    }
}
