using MediatR;

namespace Reboard.Core.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Login { get; }
        public string Password { get; }

        public CreateUserCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}