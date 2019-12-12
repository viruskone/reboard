using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain.Users;
using Reboard.Domain.Users.Commands;
using Reboard.Domain.Users.Queries;
using Reboard.Identity;

namespace Reboard.WebServer.Controllers
{
    [Route("api/user/{email?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserController(ICommandDispatcher dispatcher, IQueryDispatcher queryDispatcher)
        {
            _dispatcher = dispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetUser(string email)
            => Ok(await _queryDispatcher.HandleAsync<UserQuery, User>(new UserQuery { Email = email }));

        [HttpPost]
        public async Task<CreatedAtActionResult> Create(CreateUserRequest request)
        {
            await _dispatcher.HandleAsync(new CreateUserCommand { Request = request });
            return CreatedAtAction(nameof(GetUser), null);
        }

    }
}
