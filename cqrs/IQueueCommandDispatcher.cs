using System;
using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public interface IQueueCommandDispatcher
    {
        Task<Job> HandleAsync<TCommand>(TCommand command) where TCommand : ICommand;

        Task<Job> GetJob(Guid id);
    }
}