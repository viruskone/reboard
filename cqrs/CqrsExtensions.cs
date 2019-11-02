using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Reboard.CQRS
{
    public static class CqrsExtensions
    {

        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly forAssembly)
        {
            services.AddCommandQueryHandlers(forAssembly, typeof(ICommandHandler<>));
            services.AddCommandQueryHandlers(forAssembly, typeof(IQueryHandler<,>));
            return services;
        }

        private static void AddCommandQueryHandlers(this IServiceCollection services, Assembly assembly, Type handlerInterface)
        {
            var handlers = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }

    }
}
