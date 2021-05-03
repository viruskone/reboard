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
            var result = allReports.Where(report => report.ReportAllowedForUser(query.User));
            return result.ToList();
        }

    }


}