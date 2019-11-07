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
using System.Web.Http.Description;
using Branch.JWTProvider;
using Branch.Models;
using Branch.SearchAuxiliars;

namespace Branch.Controllers
{
    public class UserSubjectController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("user/subjects")]
        [ResponseType(typeof(List<Subject>))]
        public IHttpActionResult UserFollowedSubjects([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Subjects = UserSearchAuxiliar.FollowedSubjects(UserId);

            return Ok(Subjects);
        }

        [HttpPost]
        [Route("user/follow/subject")]
        [ResponseType(typeof(UserSubject))]
        public IHttpActionResult FollowSubject([FromUri] string AccessToken, [FromUri] int SubjectId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            var Subject = SQLContext.Subjects.Find(SubjectId);

            if(Subject == null)
            {
                return NotFound();
            }

            var AlreadyExists = UserSearchAuxiliar.FollowedSubjects(UserId).Contains(Subject);

            if(AlreadyExists)
            {
                return Ok("Already Exists");
            }

            var UserInterest = new UserSubject()
            {
                UserId = UserId,
                SubjectId = SubjectId
            };

            SQLContext.UserSubjects.Add(UserInterest);
            SQLContext.SaveChanges();

            return Ok(UserInterest);
        }

        [HttpDelete]
        [Route("user/unfollow/subject")]
        [ResponseType(typeof(UserSubject))]
        public IHttpActionResult DeleteUserSubject(int SubjectFollowId)
        {
            UserSubject UserSubject = SQLContext.UserSubjects.Find(SubjectFollowId);
            
            if (UserSubject == null)
            {
                return NotFound();
            }

            SQLContext.UserSubjects.Remove(UserSubject);
            SQLContext.SaveChanges();

            return Ok(UserSubject);
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