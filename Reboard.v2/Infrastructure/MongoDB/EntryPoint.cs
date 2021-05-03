using Microsoft.Extensions.DependencyInjection;
using Reboard.Core.Domain.Users.OutboundServices;
using Reboard.Infrastructure.MongoDB.Users;

namespace Reboard.Infrastructure.MongoDB
{
    public static class EntryPoint
    {
        public static void AddMongoDbRepositories(this IServiceCollection services, string mongoConnection, string database = "reboard")
        {
            services.AddSingleton(_ => new MongoConnection(mongoConnection, database));
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}