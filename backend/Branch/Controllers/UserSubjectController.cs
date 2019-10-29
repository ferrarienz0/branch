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

namespace Branch.Controllers
{
    public class UserSubjectController : ApiController
    {
        private readonly Context DB = new Context();

        [HttpGet]
        [Route("userInterests")]
        [ResponseType(typeof(List<Subject>))]
        public IHttpActionResult GetUserInterests([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            return Ok(DB.UserSubjects.Where(x => x.UserId == UserId).Select(x => new { x.Subject, UserInterestId = x.Id }).ToList());
        }

        [HttpPost]
        [Route("userInterests")]
        [ResponseType(typeof(UserSubject))]
        public async Task<IHttpActionResult> PostUserSubject([FromUri] string AccessToken, [FromUri] int SubjectId)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            var Subject = await DB.Subjects.FindAsync(SubjectId);

            if(Subject == null)
            {
                return NotFound();
            }

            var UserInterest = new UserSubject()
            {
                UserId = UserId,
                SubjectId = SubjectId
            };

            DB.UserSubjects.Add(UserInterest);

            await DB.SaveChangesAsync();

            return Ok(UserInterest);
        }

        [HttpDelete]
        [Route("userInterests")]
        [ResponseType(typeof(UserSubject))]
        public async Task<IHttpActionResult> DeleteUserSubject(int id)
        {
            UserSubject userSubject = await DB.UserSubjects.FindAsync(id);
            
            if (userSubject == null)
            {
                return NotFound();
            }

            DB.UserSubjects.Remove(userSubject);
            await DB.SaveChangesAsync();

            return Ok(userSubject);
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