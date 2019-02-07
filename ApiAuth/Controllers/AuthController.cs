using System;
using System.Collections.Generic;
using System.Web.Http;
namespace ApiAuth.Controllers
{
    public class AuthController : ApiController
    {
        private const string Secret =
            "4c[,y7[tXqMzhNL-a20_6hhpgl5V8Fh4k0*g9syo<6hhpgl5np,390zH48u`f+:0W333bgm6.15s8q:;C'~jc$4In8$np,390zH4BuD`YjD$tJ-e4y@>tp";
        public String Get()
        {
            IDictionary<string, object> payload = new Dictionary<string, object> {
                { "memberId",13},
                { "email","riki@gmail.com"},
                { "name","riki"},
                { "role","admin"}
            };
            TokenManager tokenManager = new TokenManager();
            return tokenManager.IssueToken(payload);
        }
        [HttpPost]
        [Authen(UserRole.Admin)]
        public string Post(string str)
        {
            return RequestContext.Principal.Identity.Name;
        }
        [HttpPut]
        [Authe2]
        [Authorize(Roles ="admin")]
        public string Put(string str)
        {
            return RequestContext.Principal.Identity.Name;
        }
    }
}
