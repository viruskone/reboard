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
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _dispatcher;
        private readonly IUniqueIdFactory _idFactory;

        public AuthController(IQueryDispatcher queryDispatcher, ICommandDispatcher dispatcher, IUniqueIdFactory idFactory)
        {
            _queryDispatcher = queryDispatcher;
            _dispatcher = dispatcher;
            _idFactory = idFactory;
        }

        [HttpPost]
        public async Task<OkObjectResult> Login(AuthenticationRequest request)
        {
            var requestId = _idFactory.Next();
            await _dispatcher.HandleAsync(new AuthenticateCommand
            {
                Id = requestId,
                Login = request.Login,
                Password = request.Password
            });
            return Ok(await _queryDispatcher.HandleAsync<AuthQuery, Auth>(new AuthQuery { Id = requestId }));
        }
    }
}