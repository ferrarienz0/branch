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
using Branch.Models.NoSQL;
using Branch.SearchAuxiliars;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Branch.Controllers
{
    public class SubjectController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("subjects")]
        [ResponseType(typeof(List<Subject>))]
        public List<Subject> GetSubjects()
        {
            var Subjects = SQLContext.Subjects.ToList();

            return Subjects;
        }

        [HttpGet]
        [Route("subjects/unfollowed")]
        [ResponseType(typeof(List<Subject>))]
        public IHttpActionResult UnfollowedSubjects([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Subjects = UserSearchAuxiliar.UnfollowedSubjects(UserId);

            return Ok(Subjects);
        }

        [HttpGet]
        [Route("subjects/followed")]
        [ResponseType(typeof(List<Subject>))]
        public IHttpActionResult FollowedSubjects([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Subjects = UserSearchAuxiliar.FollowedSubjects(UserId);

            return Ok(Subjects);
        }

        [HttpGet]
        [Route("subject")]
        [ResponseType(typeof(Subject))]
        public IHttpActionResult SubjectById(int SubjectId)
        {
            Subject Subject = SQLContext.Subjects.Find(SubjectId);

            if (Subject == null)
            {
                return NotFound();
            }

            return Ok(Subject);
        }

        [HttpPost]
        [Route("subject/create")]
        [ResponseType(typeof(Subject))]
        public IHttpActionResult CreateSubject([FromBody] Subject Subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SQLContext.Subjects.Add(Subject);
            SQLContext.SaveChanges();

            return Ok(Subject);
        }

        [HttpDelete]
        [Route("subject/delete")]
        [ResponseType(typeof(Subject))]
        public IHttpActionResult DeleteSubject(int SubjectId)
        {
            Subject Subject = SQLContext.Subjects.Find(SubjectId);
            
            if (Subject == null)
            {
                return NotFound();
            }

            SQLContext.Subjects.Remove(Subject);
            SQLContext.SaveChanges();

            return Ok(Subject);
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