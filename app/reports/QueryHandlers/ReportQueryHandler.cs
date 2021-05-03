using Reboard.CQRS;
using Reboard.Domain;
using Reboard.Domain.Reports;
using Reboard.Domain.Reports.Queries;
using System.Threading.Tasks;

namespace Reboard.App.Reports.QueryHandlers
{
    public class ReportQueryHandler : IQueryHandler<ReportQuery, Report>
    {
        private readonly IRepository<Report> _repository;

        public ReportQueryHandler(IRepository<Report> repository)
        {
            _repository = repository;
        }

        public async Task<Report> HandleAsync(ReportQuery query)
        {
            var report = await _repository.Get(query.Id);
            if (report == null) return null;
            if (report.ReportAllowedForUser(query.User) == false) return null;
            return report;
        }
    }

}