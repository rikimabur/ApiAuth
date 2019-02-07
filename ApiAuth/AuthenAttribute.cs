using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace ApiAuth
{
    public class AuthenAttribute : Attribute, IAuthenticationFilter
    {
        public AuthenAttribute(UserRole allowRoles)
        {
            AllowedRoles = allowRoles;
        }
        public bool AllowMultiple => false;
        public UserRole AllowedRoles { get; set; }
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var token = context.Request.Headers.Authorization;
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
                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "AuthenticationType"));
                    var role = (UserRole)Enum.Parse(typeof(UserRole), decoded["role"], true);
                    if ((role & AllowedRoles) != 0)
                    {
                        return Task.FromResult(0);
                    }
                }
                catch (Exception ex)
                {
                    //context.ActionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                }
            }
            context.ErrorResult = new UnauthorizedResult(Enumerable.Empty<AuthenticationHeaderValue>(), context.Request);
            return Task.FromResult(0);

        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}