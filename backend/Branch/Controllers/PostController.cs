using Branch.Models.NoSQL;
using MongoDB.Driver;
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
    public class PostController : ApiController
    {
        [HttpPost]
        [Route("post")]
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

        [HttpGet]
        [Route("posts")]
        public IHttpActionResult Index()
        {
            var MongoContext = new DataAcess();

            try
            {
                var response = MongoContext.PostCollection.Find(_ => true).ToList();
                return Ok(response);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
