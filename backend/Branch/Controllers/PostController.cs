using Branch.Models.NoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Branch.Controllers
{
    public class PostController : ApiController
    {
        [HttpPost]
        [Route("api/posts")]
        public IHttpActionResult Store([FromBody] Post NewPost)
        {
            var MongoContext = new DataAcess();
           
            try
            {
                MongoContext.PostCollection.InsertOne(NewPost);
            }
            catch
            {
                return InternalServerError();
            }

            return Ok(NewPost);
        }
    }
}
