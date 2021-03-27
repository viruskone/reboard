using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Reboard.CQRS
{
    public static class CqrsExtensions
    {



        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly forAssembly)
            => services
                .AddCommandQueryHandlers(forAssembly, typeof(ICommandHandler<>))
                .AddCommandQueryHandlers(forAssembly, typeof(IQueryHandler<,>));

        private static IServiceCollection AddCommandQueryHandlers(this IServiceCollection services, Assembly assembly, Type handlerInterface)
        {
            var handlers = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                var service = handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface);
                services.AddTransient(service, handler);
            }
            return services;
        }
    }
}