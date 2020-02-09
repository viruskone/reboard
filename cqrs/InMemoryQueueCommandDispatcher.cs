using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public class InMemoryQueueCommandDispatcher : IQueueCommandDispatcher
    {
        private readonly static ConcurrentDictionary<Guid, Job> Jobs = new ConcurrentDictionary<Guid, Job>();
        private readonly ICommandDispatcher _dispatcher;
        private readonly ILogger<InMemoryQueueCommandDispatcher> _logger;

        public InMemoryQueueCommandDispatcher(ICommandDispatcher dispatcher, ILogger<InMemoryQueueCommandDispatcher> logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public Task<Job> GetJob(Guid id)
        {
            Jobs.TryGetValue(id, out var job);
            return Task.FromResult(job);
        }

        public Task<Job> HandleAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var newJob = new Job
            {
                Id = Guid.NewGuid(),
                Status = CommandStatus.WaitingToRun,
                Command = command
            };
            Jobs.TryAdd(newJob.Id, newJob);
            Task.Factory.StartNew(async () => await WorkItem<TCommand>(newJob));
            return Task.FromResult(newJob);
        }

        private async Task WorkItem<TCommand>(Job job) where TCommand : ICommand
        {
            job.Status = CommandStatus.Running;
            try { await _dispatcher.HandleAsync((TCommand)job.Command); }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"Job {job.Id} ({job.Command.GetType().FullName}) failed");
                job.Status = CommandStatus.Faulted;
                throw;
            }
            job.Status = CommandStatus.Completed;
        }
    }
}