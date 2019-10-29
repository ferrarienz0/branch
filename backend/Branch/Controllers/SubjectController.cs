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
using Branch.Models;

namespace Branch.Controllers
{
    public class SubjectController : ApiController
    {
        private readonly Context DB = new Context();

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