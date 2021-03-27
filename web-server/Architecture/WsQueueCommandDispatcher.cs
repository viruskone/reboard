using Microsoft.AspNetCore.Http;
using Reboard.CQRS;
using System;
using System.Threading.Tasks;

namespace Reboard.WebServer.Architecture
{
    internal class WsQueueCommandDispatcher : IQueueCommandDispatcher
    {
        private readonly IQueueCommandDispatcher _dispatcher;
        private readonly IHttpContextAccessor _context;
        private readonly INotification _notifications;

        public WsQueueCommandDispatcher(IQueueCommandDispatcher dispatcher, IHttpContextAccessor contextAccessor, INotification notifications)
        {
            _dispatcher = dispatcher;
            _context = contextAccessor;
            _notifications = notifications;
        }

        public async Task<Job> HandleAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var job = await _dispatcher.HandleAsync(command);
            _notifications.RegisterJob(job);
            return job;
        }
    }

}