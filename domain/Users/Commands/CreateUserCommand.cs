using Reboard.CQRS;

namespace Reboard.Domain.Users.Commands
{
    public class CreateUserCommand : ICommand
    {
        public CreateUserRequest Request { get; set; }
    }
}