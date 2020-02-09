using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public interface ICommandDispatcher
    {
        Task HandleAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}