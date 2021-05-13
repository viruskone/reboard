using Microsoft.Extensions.DependencyInjection;
using Reboard.Core.Application.Users.DomainServices;
using Reboard.Core.Domain.Users.OutboundServices;

namespace Reboard.Core.Application.Users
{
    public static class EntryPoint
    {
        public static void AddUserApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserUniqueLoginChecker, UserUniqueLoginChecker>();
            services.AddTransient<ICompanyUniqueNameChecker, CompanyUniqueNameChecker>();
        }
    }
}