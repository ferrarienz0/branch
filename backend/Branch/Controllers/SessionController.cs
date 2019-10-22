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
        private readonly Context db = new Context();

        public class UserAuth
        {
            public string Nickname { get; set; }
            public string PasswordHash { get; set; }
            public int ValidTime { get; set; }
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }

        [HttpPost]
        [Route("session")]
        [ResponseType(typeof(TokenResponse))]
        public async Task<IHttpActionResult> PostToken([FromBody] UserAuth UserAuth)
        {
            User User = await db.Users.FirstOrDefaultAsync(u => u.Nickname == UserAuth.Nickname && u.Password == UserAuth.PasswordHash);
            
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

            var Response = new TokenResponse
            {
                Token = Token
            };

            return Ok(Response);
        }
    }
}
