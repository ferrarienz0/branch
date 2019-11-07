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
using System.Data.Entity;

namespace Branch.Controllers
{
    public class SessionController : ApiController
    {
        private readonly SQLContext SQLContext = new SQLContext();

        public class UserAuth
        {
            public string Nickname { get; set; }
            public string PasswordHash { get; set; }
            public int ValidTime { get; set; }
        }

        [HttpPost]
        [Route("session")]
        public IHttpActionResult PostToken([FromBody] UserAuth UserAuth)
        {
            User User = SQLContext.Users.FirstOrDefault(u => u.Nickname == UserAuth.Nickname && u.Password == UserAuth.PasswordHash);
            
            if (User == default)
            {
                return NotFound();
            }

            var NewToken = new Token(
                new Dictionary<string, object>()
                {
                    {"id", User.Id}
                }
            );

            NewToken.SetExpiration(UserAuth.ValidTime);
            
            var Token = NewToken.CreateToken(UserAuth.ValidTime);

            var Response = new { Token };

            return Ok(Response);
        }
    }
}
