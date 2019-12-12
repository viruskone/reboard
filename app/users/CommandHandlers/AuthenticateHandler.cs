using System;
using System.Threading.Tasks;
using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Auth.Commands;

namespace Reboard.App.Users.CommandHandlers
{
    public class AuthenticateHandler : ICommandHandler<AuthenticateCommand>
    {
        private readonly IUserService _userService;

        public AuthenticateHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task HandleAsync(AuthenticateCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
