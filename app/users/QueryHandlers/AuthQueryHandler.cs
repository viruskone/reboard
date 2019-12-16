using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Auth;
using Reboard.Domain.Auth.Queries;
using System;
using System.Threading.Tasks;

namespace Reboard.App.Users.QueryHandlers
{
    public class AuthQueryHandler : IQueryHandler<AuthQuery, Auth>
    {
        private readonly IAuthService _authService;

        public AuthQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Auth> HandleAsync(AuthQuery query)
            => await _authService.Get(query.Id);
    }
}
