using Branch.JWTProvider;
using Branch.Models;
using Branch.Models.NoSQL;
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

        [HttpPost]
        [Route("post")]
        public async Task<IHttpActionResult> PostPost([FromBody] Post NewPost, [FromUri] string AccessToken)
        {  
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = TokenValidator.VerifyToken(AccessToken);
            NewPost.UserId = UserId;

            var Hashtags = FindTag(NewPost.Text, "#");
            var Mentions = FindTag(NewPost.Text, "@");
            var Products = FindTag(NewPost.Text, "$");

            var HashtagObjects = await CheckHashtagsExistence(Hashtags);
            var MentionObjects = await CheckMentionsExistence(Mentions);
            var ProductsObjects = await CheckProductExistence(Products);

            NewPost.Hashtags = HashtagObjects.Select(x => x.Id).ToList();
            NewPost.Mentions = MentionObjects.Select(x => x.Id).ToList();
            NewPost.Products = ProductsObjects.Select(x => x.Id).ToList();
 
            await MongoContext.PostCollection.InsertOneAsync(NewPost);

            dynamic Response = new { NewPost, HashtagObjects, MentionObjects, ProductsObjects };

            return Ok(Response);
        }

        [HttpGet]
        [Route("posts")]
        [ResponseType(typeof(List<Post>))]
        public async Task<IHttpActionResult> GetPosts([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var UserFollowings = DB.Follows.Where(x => x.FollowerId == UserId).Select(x => x.Id);
            var UserInterests = DB.UserSubjects.Where(x => x.UserId == UserId).Select(x => x.Id);
            
            var RecommendedPosts = await MongoContext.PostCollection.FindAsync(x =>
                  x.UserId == UserId
                 || UserFollowings.Contains(x.UserId)
                 || x.Mentions.Exists(y => UserFollowings.Contains(y))
                 || x.Hashtags.Exists(z => UserInterests.Contains(z))
            );
                
            if(RecommendedPosts == null)
            {
                return NotFound();
            }

            return Ok(RecommendedPosts.ToList());
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
    }
}
