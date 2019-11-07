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
using MongoDB.Bson;
using MongoDB.Driver;

namespace Branch.Controllers
{
    public class SubjectController : ApiController
    {
        private readonly SQLContext DB = new SQLContext();
        private readonly NoSQLContext MongoContext = new NoSQLContext();

        [HttpGet]
        [Route("subject")]
        [ResponseType(typeof(List<Subject>))]
        public List<Subject> GetSubjects()
        {
            var Subjects = DB.Subjects.ToList();

            return Subjects;
        }

        [HttpGet]
        [Route("subject")]
        [ResponseType(typeof(List<Subject>))]
        public List<Subject> GetUnfollowedSubjects([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            var Subjects = DB.Subjects.Where(x => DB.UserSubjects.Where(y => y.SubjectId == x.Id && y.UserId == UserId).Count() == 0).ToList();

            return Subjects;
        }

        [HttpGet]
        [Route("subject")]
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> GetSubject(int id)
        {
            Subject subject = await DB.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        [HttpPost]
        [Route("subject")]
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> PostSubject([FromBody] Subject Subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DB.Subjects.Add(Subject);
            await DB.SaveChangesAsync();

            return Ok(Subject);
        }

        [HttpGet]
        [Route("subject/posts")]
        [ResponseType(typeof(List<Post>))]
        public IHttpActionResult GetSubjectPosts([FromUri] int SubjectId)
        {
            var Posts = MongoContext.PostCollection.Find(x => x.Hashtags.Contains(SubjectId)).ToList();

            return Ok(Posts);
        }

        [HttpDelete]
        [Route("subject")]
        [ResponseType(typeof(Subject))]
        public async Task<IHttpActionResult> DeleteSubject(int id)
        {
            Subject subject = await DB.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            DB.Subjects.Remove(subject);
            await DB.SaveChangesAsync();

            return Ok(subject);
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