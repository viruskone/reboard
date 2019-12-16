using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Commands;
using Reboard.Identity;
using System.Threading.Tasks;

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
            await CreateUser(request.Email);
            await SetPassword(request.Email, request.Password);
        }

        private async Task CreateUser(string email)
        {
            try
            {
                await _service.Create(email);
            }
            catch (UserException exception) when (exception.Type == UserException.ErrorType.UserAlreadyExist) { }
        }

        private async Task SetPassword(string email, string password)
            => await _service.SetPassword(email, _hashing.Encrypt(password));
    }
}