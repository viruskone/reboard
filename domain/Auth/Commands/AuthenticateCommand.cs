using System;
using Reboard.CQRS;

namespace Reboard.Domain.Auth.Commands
{
    public class AuthenticateCommand : ICommand
    {

        public string Login { get; set; }
        public string Password { get; set; }

    }
}
