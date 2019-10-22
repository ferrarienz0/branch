using JWT;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Branch.JWTProvider
{
    public static class TokenValidator
    {
        private static readonly string Secret = "RumoAos50K";

        public static string VerifyToken(string Token)
        {
            IJsonSerializer JSONSerializer = new JsonNetSerializer();
            IDateTimeProvider TimeProvider = new UtcDateTimeProvider();
            IBase64UrlEncoder URLEncoder = new JwtBase64UrlEncoder();
            IJwtValidator Validator = new JwtValidator(JSONSerializer, TimeProvider);
            IJwtDecoder Decoder = new JwtDecoder(JSONSerializer, Validator, URLEncoder);
            
            var JSON = Decoder.Decode(Token, Secret, verify: true);

            return JSON;
        }
    }
}