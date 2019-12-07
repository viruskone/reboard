using System;
using System.Threading.Tasks;
using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Users.Commands;
using Reboard.Identity;

namespace Reboard.App.Users.CommandHandlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserService _service;
        private readonly IHashService _hashing;

        public CreateUserHandler(IUserService service, IHashService hashService)
        {
            _service = service;
            _hashing = hashService;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var request = command.Request;
            await _service.Create(request.Email);
            await _service.SetPassword(request.Email, _hashing.Encrypt(request.Password));
        }
    }
}
