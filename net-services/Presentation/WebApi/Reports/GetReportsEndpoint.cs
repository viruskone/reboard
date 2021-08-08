using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reboard.Core.Application.Reports.GetReports;
using Reboard.Presentation.WebApi.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Presentation.WebApi.Reports
{
    [Route(Routes.ReportsRoute)]
    [Authorize]
    public class GetReportsEndpoint : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IEnumerable<ReportDto>>
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public GetReportsEndpoint(IMediator mediator, IUserAccessor userAccessor)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ReportDto>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var user = _userAccessor.Get();
            if (user == null) return Unauthorized();
            var query = new GetReportsQuery(user.UserId, user.CompanyId);
            return Ok(await _mediator.Send(query));
        }
    }
}