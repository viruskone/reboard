using Reboard.CQRS;

namespace Reboard.Domain.Dummy.Commands
{
    public class DummyCommand : ICommand
    {
        public string PastAsResponse { get; set; }
    }
}
