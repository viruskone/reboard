using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.Domain.Auth;
using Reboard.Domain.Auth.Commands;
using Reboard.Domain.Auth.Queries;
using Reboard.WebServer.Architecture;
using System;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/auth/{requestId?}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IQueueCommandDispatcher _dispatcher;
        private readonly IUniqueIdFactory _idFactory;

        public AuthController(IQueryDispatcher queryDispatcher, IQueueCommandDispatcher dispatcher, IUniqueIdFactory idFactory)
        {
            _queryDispatcher = queryDispatcher;
            _dispatcher = dispatcher;
            _idFactory = idFactory;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetAuth(string requestId)
            => Ok(await _queryDispatcher.HandleAsync<AuthQuery, Auth>(new AuthQuery { Id = requestId }));


        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            var requestId = _idFactory.Next();
            var job = await _dispatcher.HandleAsync(new AuthenticateCommand
            {
                Id = requestId,
                Login = request.Login,
                Password = request.Password
            });
            job.RegisterResourceUrl(Url.ActionLink(action: nameof(GetAuth), values: new { requestId }));
            return this.AcceptedAtTask(job.Id);
        }
    }
}