using Microsoft.AspNetCore.Http;
using Reboard.Core.Application.Users.Authenticate;
using System.Text.Json;

namespace Reboard.Presentation.WebApi.Users
{
    public class JwtUserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public JwtUserAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public UserClaims Get()
        {
            var userContext = _contextAccessor.HttpContext.User;
            if (userContext.Identity.IsAuthenticated == false) return null;
            var claim = userContext.FindFirst("user");
            if (claim == null) return null;
            return JsonSerializer.Deserialize<UserClaims>(claim.Value);
        }
    }
}