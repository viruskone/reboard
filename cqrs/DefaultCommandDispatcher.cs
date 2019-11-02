using System;
using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public class DefaultCommandDispatcher : ICommandDispatcher
    {

        private readonly IServiceProvider _services;

        public DefaultCommandDispatcher(IServiceProvider services)
        {
            _services = services;
        }

        public async Task HandleAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _services.GetService(typeof(ICommandHandler<TCommand>)) as ICommandHandler<TCommand>;
            await handler.HandleAsync(command);
        }
    }
}
