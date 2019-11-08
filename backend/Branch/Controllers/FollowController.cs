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
using Branch.SearchAuxiliars;
using Npgsql;

namespace Branch.Controllers
{
    public class FollowController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("follows")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult UserFollows([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Follows = UserSearchAuxiliar.Follows(UserId, SQLContext);

            return Ok(Follows);
        }

        [HttpGet]
        [Route("followers")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult UserFollowers([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Followers = UserSearchAuxiliar.Followers(UserId, SQLContext);

            return Ok(Followers);
        }

        [HttpPost]
        [Route("follow/create")]
        [ResponseType(typeof(Follow))]
        public IHttpActionResult CreateFollow([FromUri] string AccessToken, [FromUri] int RequestedUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserId = TokenValidator.VerifyToken(AccessToken);

            var AlreadyExists = SQLContext.Follows
                                                  .Where(x => x.FollowedId == RequestedUserId && x.FollowerId == UserId)
                                                  .Any();

            if (AlreadyExists)
            {
                return Ok("Already Exists");
            }

            var Follow = new Follow()
            {
                FollowerId = UserId,
                FollowedId = RequestedUserId
            };  

            SQLContext.Follows.Add(Follow);
            SQLContext.SaveChanges();

            return Ok(Follow);
        }

        [HttpDelete]
        [Route("follow/delete")]
        public IHttpActionResult DeleteFollow([FromUri] string AccessToken, [FromUri] int FollowedId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            Follow Follow = SQLContext.Follows
                                              .Where(x => x.FollowerId == UserId && x.FollowedId == FollowedId)
                                              .FirstOrDefault();

            if (Follow == null)
            {
                return NotFound();
            }

            SQLContext.Follows.Remove(Follow);
            SQLContext.SaveChanges();

            return Ok();
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