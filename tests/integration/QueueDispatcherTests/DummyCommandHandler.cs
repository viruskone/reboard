using Reboard.CQRS;
using Reboard.Domain.Dummy.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.IntegrationTests.QueueDispatcherTests
{
    public class DummyCommandHandler : ICommandHandler<DummyCommand>
    {

        internal static ManualResetEvent Event { get; } = new ManualResetEvent(false);

        public Task HandleAsync(DummyCommand command)
        {
            Event.WaitOne();
            return Task.CompletedTask;
        }
    }

}
