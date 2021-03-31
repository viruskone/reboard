using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Queries;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/user/{login?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public UserController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetUser(string login)
            => Ok(await _queryDispatcher.HandleAsync<UserQuery, User>(new UserQuery { Login = login }));

    }
}