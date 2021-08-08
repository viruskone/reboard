using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Reports.OutboundServices;
using Reboard.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reboard.Tests.WebApi.IntegrationTests.Mocks
{
    public class MemoryReportRepository : IReportRepository
    {
        private readonly Dictionary<ReportId, Report> _reports = new Dictionary<ReportId, Report>();

        public Task<IEnumerable<Report>> GetByCompany(Guid companyId)
            => Task.FromResult(_reports.Values.Where(report => report.AllowedCompanies.Contains((CompanyId)companyId)));

        public Task<Report> GetByTitle(ReportTitle title)
            => Task.FromResult(_reports.Values.FirstOrDefault(report => report.Title == title));

        public Task<IEnumerable<Report>> GetByUser(Guid userId)
            => Task.FromResult(_reports.Values.Where(report => report.AllowedUsers.Contains((UserId)userId)));

        public Task Save(Report report)
        {
            _reports[report.Id] = report;
            return Task.CompletedTask;
        }
    }
}