using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ApiAuth
{
    public class Authe2Attribute : AuthorizeAttribute
    {
        public Authe2Attribute(UserRole allowRoles = UserRole.Admin)
        {
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization;
            if (token != null)
            {
                TokenManager tokenManager = new TokenManager();
                try
                {
                    IDictionary<string, string> decoded = tokenManager.DecodeToken(token.ToString());
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,decoded["name"]),
                        new Claim(ClaimTypes.Email,decoded["email"]),
                        new Claim(ClaimTypes.Role,decoded["role"])
                    };
                    actionContext.RequestContext.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims));
                }
                catch (Exception ex)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

            }
            base.OnAuthorization(actionContext);
        }
    }
}