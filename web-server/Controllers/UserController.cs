using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Commands;
using Reboard.Domain.Users.Queries;
using Reboard.WebServer.Architecture;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/user/{email?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IQueueCommandDispatcher _dispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserController(IQueueCommandDispatcher dispatcher, IQueryDispatcher queryDispatcher)
        {
            _dispatcher = dispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetUser(string email)
            => Ok(await _queryDispatcher.HandleAsync<UserQuery, User>(new UserQuery { Email = email }));

    }
}