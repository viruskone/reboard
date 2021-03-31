using Microsoft.AspNetCore.Http;
using Reboard.Domain.Users;
using System.Text.Json;

namespace Reboard.WebServer.Architecture
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public User Get()
        {
            var userContext = _contextAccessor.HttpContext.User;
            if (userContext.Identity.IsAuthenticated == false) return null;
            var claim = userContext.FindFirst("user");
            if (claim == null) return null;
            return JsonSerializer.Deserialize<User>(claim.Value);
        }
    }

}
