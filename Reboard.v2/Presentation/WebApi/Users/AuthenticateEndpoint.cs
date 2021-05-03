using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reboard.Core.Application.Users.Authenticate;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Presentation.WebApi.Users
{
    [Route(Routes.UsersRoute)]
    public class AuthenticateEndpoint : BaseAsyncEndpoint
        .WithRequest<AuthenticateRequest>
        .WithResponse<AuthenticateResponse>
    {
        private readonly IMediator _mediator;

        public AuthenticateEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
            => Ok(await _mediator.Send(new AuthenticateCommand(request.Login, request.Password), cancellationToken));
    }
}