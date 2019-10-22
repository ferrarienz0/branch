using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.JWTProvider
{
    public class Token
    {
        private readonly string Secret = "RumoAos50K";
        private readonly Dictionary<string, object> Payload;
        private readonly IJwtAlgorithm EncriptionAlgorithm;
        private readonly IJsonSerializer JSONSerializer;
        private readonly IBase64UrlEncoder URLEncoder;
        private readonly IJwtEncoder JWTEncoder;

        public Token(Dictionary<string, object> Payload)
        {
            this.Payload = Payload;
            EncriptionAlgorithm = new HMACSHA256Algorithm();
            JSONSerializer = new JsonNetSerializer();
            URLEncoder = new JwtBase64UrlEncoder();
            JWTEncoder = new JwtEncoder(EncriptionAlgorithm, JSONSerializer, URLEncoder);
        }

        public void SetExpiration(int Minutes)
        {
            IDateTimeProvider TimeProvider = new UtcDateTimeProvider();
            var Now = TimeProvider.GetNow();
            var SecondsSinceEpoch = UnixEpoch.GetSecondsSince(Now);
            var ExpirationTime = SecondsSinceEpoch + (Minutes * 60);

            this.Payload.Add("exp", ExpirationTime);
        }

        public string CreateToken(int ValidTo)
        {
            string Token = this.JWTEncoder.Encode(this.Payload, this.Secret);

            return Token;
        }
    }
}