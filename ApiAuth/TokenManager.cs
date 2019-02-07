using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Collections.Generic;

namespace ApiAuth
{
    public class TokenManager
    {
        private const string Secret =
            "4c[,y7[tXqMzhNL-a20_6hhpgl5V8Fh4k0*g9syo<6hhpgl5np,390zH48u`f+:0W333bgm6.15s8q:;C'~jc$4In8$np,390zH4BuD`YjD$tJ-e4y@>tp";
        public string IssueToken(IDictionary<string, object> parameters)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder base64UrlEncoder = new JwtBase64UrlEncoder();
            JwtEncoder encoder = new JwtEncoder(algorithm, serializer, base64UrlEncoder);
            var token = encoder.Encode(parameters, Secret);
            return token;
        }
        public IDictionary<string, string> DecodeToken(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider dateTimeProvider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, dateTimeProvider);
            IBase64UrlEncoder base64UrlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, base64UrlEncoder);
            var decoded = decoder.DecodeToObject<Dictionary<string, string>>(token, Secret, true);
            return decoded;
        }
    }
}