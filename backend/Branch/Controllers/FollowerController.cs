using Branch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Branch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FollowerController : ApiController
    {
        [HttpPost]
        [Route("follower")]
        public IHttpActionResult Store([FromBody] Follow NewFollow)
        {
            var SQLContext = new Context();

            try
            {
                var Response = SQLContext.Follows.Add(NewFollow);
                SQLContext.SaveChanges();
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
        [Route("follower")]
        public IHttpActionResult Index([FromUri] int UserId)
        {
            var SQLContext = new Context();

            try
            {
                var Response = SQLContext.Follows.Where(x => x.IDFollowed.ID == UserId).ToList();
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
