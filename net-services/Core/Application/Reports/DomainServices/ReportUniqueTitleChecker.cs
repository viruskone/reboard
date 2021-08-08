using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Reports.OutboundServices;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Reports.DomainServices
{
    public class ReportUniqueTitleChecker : IReportUniqueTitleChecker
    {
        private readonly IReportRepository _repository;

        public ReportUniqueTitleChecker(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUnique(ReportTitle title)
            => await _repository.GetByTitle(title) == null;
    }
}