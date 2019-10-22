using System;
using System.Collections.Generic;
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
    public class SessionController : ApiController
    {
        private readonly Context db = new Context();

        public class UserAuth
        {
            public int Id { get; set; }
            public string PasswordHash { get; set; }
            public int ValidTime { get; set; }
        }

        [HttpGet]
        [Route("session")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetToken([FromBody] UserAuth UserAuth)
        {
            User User = await db.Users.FindAsync(UserAuth.Id);
            
            if (User == null || User.Password != UserAuth.PasswordHash)
            {
                return NotFound();
            }

            var NewToken = new Token(
                new Dictionary<string, object>()
                {
                    {"id", UserAuth.Id}
                }
            );

            NewToken.SetExpiration(UserAuth.ValidTime);
            var Response = NewToken.CreateToken(UserAuth.ValidTime);

            return Ok(Response);
        }
    }
}
