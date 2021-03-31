using Reboard.CQRS;
using Reboard.Domain;
using Reboard.Domain.Reports;
using Reboard.Domain.Reports.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reboard.App.Reports.QueryHandlers
{
    public class ReportsQueryHandler : IQueryHandler<ReportsQuery, IEnumerable<Report>>
    {
        private readonly IRepository<Report> _repository;

        public ReportsQueryHandler(IRepository<Report> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Report>> HandleAsync(ReportsQuery query)
        {
            var allReports = await _repository.GetAll();
            var result = allReports.Where(report => ReportAllowedForAll(report) || ReportAllowedForUser(report, query));
            return result.ToList();
        }

        private bool ReportAllowedForAll(Report report) => report.AllowedUsers.Contains("*");

        private bool ReportAllowedForUser(Report report, ReportsQuery query) =>
            report.AllowedUsers.Contains(query.ForUser)
            || report.AllowedCompanies.Contains(query.ForCompany);
    }
}