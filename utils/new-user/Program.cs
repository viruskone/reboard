using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.CQRS;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Commands;
using Reboard.Utils.ConsoleBase;
using Reboard.WebServer;

namespace Reboard.Utils.NewUser
{
    public class Program : IProgram
    {
        private readonly ICommandDispatcher _dispatcher;

        public Program(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        static async Task Main(string[] args)
        {
            var runner = new UtilRunner<Program>(options =>
            {
                options
                    .SetTitle("Tool to add new user");
            });
            await runner.Execute();
        }

        public async Task Run(UtilMethods methods)
        {
            var email = methods.Ask("e-mail");
            var password = methods.Ask("password");
            
            methods.Processing();
            await _dispatcher.HandleAsync(new CreateUserCommand
            {
                Request = new CreateUserRequest
                {
                    Email = email,
                    Password = password
                }
            });
        }
    }
}
