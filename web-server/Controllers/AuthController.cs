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
    [Route("api/auth/{id?}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IQueueCommandDispatcher _dispatcher;

        public AuthController(IQueryDispatcher queryDispatcher, IQueueCommandDispatcher dispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetAuth(string id)
            => Ok(await _queryDispatcher.HandleAsync<AuthQuery, Auth>(new AuthQuery { Id = id }));


        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            var requestId = Guid.NewGuid().ToString();
            var job = await _dispatcher.HandleAsync(new AuthenticateCommand
            {
                Id = requestId,
                Login = request.Login,
                Password = request.Password
            });
            job.RegisterResourceUrl(Url.Action(nameof(GetAuth), new { id = requestId }));
            return this.AcceptedAtTask(job.Id);
        }
    }
}