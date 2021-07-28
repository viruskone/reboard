using Microsoft.Extensions.DependencyInjection;
using Reboard.Core.Application.Reports.DomainServices;
using Reboard.Core.Domain.Reports.OutboundServices;

namespace Reboard.Core.Application.Reports
{
    public static class EntryPoint
    {
        public static void AddReportApplication(this IServiceCollection services)
        {
            services.AddTransient<IReportUniqueTitleChecker, ReportUniqueTitleChecker>();
        }
    }
}