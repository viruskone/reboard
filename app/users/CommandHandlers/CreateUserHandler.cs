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
            await CreateUser(new User
            {
                Login = command.Request.Login,
                Company = command.Request.Company
            });
            await SetPassword(request.Login, request.Password);
        }

        private async Task CreateUser(User user)
        {
            try
            {
                await _service.Create(user);
            }
            catch (UserException exception) when (exception.Type == UserException.ErrorType.UserAlreadyExist) { }
        }

        private async Task SetPassword(string login, string password)
            => await _service.SetPassword(login, _hashing.Encrypt(password));
    }
}