using Branch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Branch.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("user")]
        public IHttpActionResult Store([FromBody] User NewUser)
        {
            var SQLContext = new Context();

            try
            {
                var Response = SQLContext.Users.Add(NewUser);
                SQLContext.SaveChangesAsync();
                SQLContext.Dispose();
                
                return Ok(Response);
            }
            catch
            {
                SQLContext.Dispose();
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("user")]
        public IHttpActionResult Index([FromUri] int UserId)
        {
            var SQLContext = new Context();

            try
            {
                var Response = SQLContext.Users.Where(x => x.ID == UserId).ToList();
                SQLContext.SaveChangesAsync();
                SQLContext.Dispose();

                return Ok(Response);
            }
            catch
            {
                SQLContext.Dispose();
                return InternalServerError();
            }
        }
    }
}
