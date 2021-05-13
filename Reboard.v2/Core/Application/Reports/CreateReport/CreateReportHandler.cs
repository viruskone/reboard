using MediatR;
using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Reports.OutboundServices;
using Reboard.Core.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Reports.CreateReport
{
    public class CreateReportHandler : IRequestHandler<CreateReportCommand>
    {
        private readonly IReportUniqueTitleChecker _checker;
        private readonly IReportRepository _repository;

        public CreateReportHandler(IReportRepository repository, IReportUniqueTitleChecker checker)
        {
            _repository = repository;
            _checker = checker;
        }

        public async Task<Unit> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var report = Report.CreateNew(
                request.Title,
                request.Description,
                request.Shortcut,
                Color.Make(request.Color.Red, request.Color.Green, request.Color.Blue),
                _checker
            );
            foreach (var userId in request.AllowedUsers)
            {
                report.AllowUser((UserId)userId);
            }
            foreach (var companyId in request.AllowedCompanies)
            {
                report.AllowAllCompanyUsers((CompanyId)companyId);
            }

            await _repository.Save(report);
            return Unit.Value;
        }
    }
}