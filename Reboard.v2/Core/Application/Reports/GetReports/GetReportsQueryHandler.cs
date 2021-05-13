using MediatR;
using Reboard.Core.Domain.Reports.OutboundServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Reports.GetReports
{
    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, IEnumerable<ReportDto>>
    {
        private readonly IReportRepository _repository;

        public async Task<IEnumerable<ReportDto>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            var reportsByUser = await _repository.GetByUser(request.UserId);
            var reportsByCompany = await _repository.GetByCompany(request.CompanyId);
            return reportsByUser.Concat(reportsByCompany).Select(report => report.ToDto());
        }
    }
}