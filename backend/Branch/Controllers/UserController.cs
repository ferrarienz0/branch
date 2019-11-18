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
using Branch.Models.NoSQL;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Branch.Controllers
{
    public class UserController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        [HttpGet]
        [Route("users")]
        public List<User> GetUsers()
        {
            return SQLContext.Users.ToList();
        }

        [HttpGet]
        [Route("user")]
        [ResponseType(typeof(User))]
        public IHttpActionResult UserById(int UserId)
        {
            User User = SQLContext.Users.Find(UserId);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        [HttpGet]
        [Route("user")]
        [ResponseType(typeof(User))]
        public IHttpActionResult UserByToken([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);

            User User = SQLContext.Users.Find(UserId);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        [HttpPut]
        [Route("user/update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateUser([FromUri] int UserId, [FromBody] User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UserId != User.Id)
            {
                return BadRequest();
            }

            SQLContext.Entry(User).State = EntityState.Modified;

            try
            {
                SQLContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserId))
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

        [HttpPut]
        [Route("user/pro")]
        [ResponseType(typeof(void))]
        public IHttpActionResult MakeUserPro([FromUri] string AccessToken)
        {
            var UserId = TokenValidator.VerifyToken(AccessToken);
            var User = SQLContext.Users.Find(UserId);

            if(User == null)
            {
                return NotFound();
            }

            User.IsPro = true;

            SQLContext.Entry(User).State = EntityState.Modified;
            SQLContext.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("user/create")]
        [ResponseType(typeof(User))]
        public IHttpActionResult CreateUser(User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SQLContext.Users.Add(User);
            SQLContext.SaveChanges();

            return Ok(User);
        }


        [HttpDelete]
        [Route("user/delete")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int UserId)
        {
            User User = SQLContext.Users.Find(UserId);
            
            if (User == null)
            {
                return NotFound();
            }

            SQLContext.Users.Remove(User);
            SQLContext.SaveChanges();

            return Ok(User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SQLContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return SQLContext.Users.Any(e => e.Id == id);
        }
    }
}