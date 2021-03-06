using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Reboard.CQRS
{
    public class DefaultQueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _services;

        public DefaultQueryDispatcher(IServiceProvider services)
        {
            _services = services;
        }

        public async Task<TResult> HandleAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _services.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query);
        }
    }
}