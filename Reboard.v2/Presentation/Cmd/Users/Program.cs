using MediatR;
using Reboard.Core.Application.Users.CreateUser;
using Reboard.Presentation.Cmd.CmdBase;
using System.Threading.Tasks;

namespace Reboard.Presentation.Cmd.Users
{
    public class Program : IProgram
    {
        private readonly IMediator _mediator;

        public Program(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Run(CommandLineMethods methods)
        {
            var email = methods.Ask("e-mail");
            var password = methods.Ask("password");

            methods.Processing();
            await _mediator.Send(new CreateUserCommand(email, password));
        }

        private static async Task Main(string[] args)
        {
            var runner = new CommandLineRunner<Program>(options =>
            {
                options
                    .SetTitle("CMD to add new user");
            });
            await runner.Execute();
        }
    }
}