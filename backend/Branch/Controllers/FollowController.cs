using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Branch.JWTProvider;
using Branch.Models;
using Npgsql;

namespace Branch.Controllers
{
    public class FollowController : ApiController
    {
        private readonly SQLContext DB = new SQLContext();
        private readonly string ConnectionString = "server=tuffi.db.elephantsql.com;User Id=vlvhqsdd;Database=vlvhqsdd;Port=5432;Password=0pBsOXETTteJOJt6Ysf0jq_135BK--N3";

        [HttpGet]
        [Route("follow")]
        [ResponseType(typeof(List<dynamic>))]
        public IHttpActionResult GetUserFollows([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Follows = DB.Follows.Where(x => x.FollowerId == UserId).Select(x => x.Followed);

            return Ok(Follows);
        }

        [HttpGet]
        [Route("followers")]
        [ResponseType(typeof(List<User>))]
        public List<User> GetUserFollowers([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            return DB.Follows.Where(x => x.FollowedId == UserId).Select(x => x.Follower).ToList();
        }

        [HttpPost]
        [Route("follow")]
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> PostFollow([FromUri] string AccessToken, [FromUri] int RequestedUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = TokenValidator.VerifyToken(AccessToken);

            var AlreadyExists = DB.Follows.ToList().Where(x => x.FollowedId == RequestedUserId && x.FollowerId == UserId).Count() > 0;

            if (AlreadyExists)
            {
                return Ok("Already Exists");
            }

            var Follow = new Follow()
            {
                FollowerId = UserId,
                FollowedId = RequestedUserId
            };  

            DB.Follows.Add(Follow);

            await DB.SaveChangesAsync();

            return Ok(Follow);
        }

        [HttpDelete]
        [Route("follow")]
        public async Task<IHttpActionResult> DeleteFollow([FromUri] string AccessToken, [FromUri] int FollowedId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            Follow follow = DB.Follows.Where(x => x.FollowerId == UserId && x.FollowedId == FollowedId).FirstOrDefault();

            if (follow == null)
            {
                return NotFound();
            }

            DB.Follows.Remove(follow);
            await DB.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}