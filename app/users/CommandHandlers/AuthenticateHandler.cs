using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Auth.Commands;
using Reboard.Identity;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reboard.App.Users.CommandHandlers
{
    public class AuthenticateHandler : ICommandHandler<AuthenticateCommand>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IHashService _hashService;
        private readonly ITokenFactory _tokenFactory;

        public AuthenticateHandler(IUserService userService, IAuthService authService, IHashService hashService, ITokenFactory tokenFactory)
        {
            _userService = userService;
            _authService = authService;
            _hashService = hashService;
            _tokenFactory = tokenFactory;
        }

        public async Task HandleAsync(AuthenticateCommand command)
        {
            var valid = await _userService.Validate(command.Login, _hashService.Encrypt(command.Password));
            if (!valid)
            {
                await _authService.Failed(command.Id, command.Login);
                return;
            }
            var user = await _userService.Get(command.Login);
            var token = _tokenFactory.Create();
            token.SetName(command.Login);
            token.AddClaim("user", JsonSerializer.Serialize(user));
            token.SetExpiration(TimeSpan.FromDays(7));
            await _authService.Success(command.Id, command.Login, token.Generate());
        }
    }
}