using Branch.Models.NoSQL;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Branch.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostController : ApiController
    {
        private readonly DataAcess MongoContext = new DataAcess();

        [HttpPost]
        [Route("post")]
        public async Task<IHttpActionResult> PostPost([FromBody] Post NewPost)
        {  
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await MongoContext.PostCollection.InsertOneAsync(NewPost);

            return CreatedAtRoute("DefaultApi", new { id = NewPost.ID }, NewPost);
        }

        [HttpGet]
        [Route("posts")]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetPosts()
        {
            var Posts = await MongoContext.PostCollection.FindAsync(_ => true);
                
            if(Posts == null)
            {
                return NotFound();
            }

            return Ok(Posts);
        }
    }
}
