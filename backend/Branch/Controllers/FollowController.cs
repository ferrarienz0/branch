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
using Branch.Models;

namespace Branch.Controllers
{
    public class FollowController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        [Route("follow")]
        public IQueryable<Follow> GetFollows()
        {
            return db.Follows;
        }

        [HttpGet]
        [Route("follow")]
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> GetFollow(int id)
        {
            Follow follow = await db.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            return Ok(follow);
        }

        [HttpPut]
        [Route("follow")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollow(int id, Follow follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != follow.Id)
            {
                return BadRequest();
            }

            db.Entry(follow).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("follow")]
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> PostFollow(Follow follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Follows.Add(follow);
            await db.SaveChangesAsync();

            return Ok(follow);
        }

        [HttpDelete]
        [Route("follow")]
        [ResponseType(typeof(Follow))]
        public async Task<IHttpActionResult> DeleteFollow(int id)
        {
            Follow follow = await db.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            db.Follows.Remove(follow);
            await db.SaveChangesAsync();

            return Ok(follow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowExists(int id)
        {
            return db.Follows.Count(e => e.Id == id) > 0;
        }
    }
}