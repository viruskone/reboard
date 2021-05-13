using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reboard.Core.Application.Reports.GetReports;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Presentation.WebApi.Reports
{
    [Route(Routes.ReportsRoute)]
    public class GetReportsEndpoint : BaseAsyncEndpoint
        .WithRequest<GetReportsQuery>
        .WithResponse<IEnumerable<ReportDto>>
    {
        private readonly IMediator _mediator;

        public GetReportsEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ReportDto>>> HandleAsync(GetReportsQuery request, CancellationToken cancellationToken = default)
        {
            var query = new GetReportsQuery(request.UserId, request.CompanyId);
            return Ok(await _mediator.Send(query));
        }
    }
}