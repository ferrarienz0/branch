using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Branch.JWTProvider;
using Branch.Models;

namespace Branch.Controllers
{
    public class FollowController : ApiController
    {
        private readonly Context DB = new Context();

        [HttpGet]
        [Route("follow")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetUserFollows([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            return Ok(DB.Follows.Where(x => x.FollowerId == UserId).Select(x => new { x.Followed, FollowId = x.Id }).ToList());
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
                FollowedId = RequestedUserId,
                Follower = DB.Users.Find(UserId),
                Followed = DB.Users.Find(RequestedUserId)
            };  

            DB.Follows.Add(Follow);

            await DB.SaveChangesAsync();

            return Ok(Follow);
        }

        [HttpDelete]
        [Route("follow")]
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> DeleteFollow(int id)
        {
            Follow follow = await DB.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            DB.Follows.Remove(follow);
            await DB.SaveChangesAsync();

            return Ok(follow);
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