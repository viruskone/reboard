using MediatR;
using Reboard.Core.Domain.Users;

namespace Reboard.Core.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string CompanyName { get; }
        public string Login { get; }
        public string Password { get; }

        public CreateUserCommand(string login, string password, string companyName)
        {
            Login = login;
            Password = password;
            CompanyName = companyName;
        }
    }
}