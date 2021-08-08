using MediatR;

namespace Reboard.Core.Application.Users.Authenticate
{
    public class AuthenticateCommand : IRequest<AuthenticateResponse>
    {
        public string Login { get; }
        public string Password { get; }

        public AuthenticateCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}