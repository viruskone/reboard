using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.Domain.Dummy;
using Reboard.Domain.Dummy.Commands;
using Reboard.WebServer.Architecture;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/dummy")]
    [ApiController]
    [Authorize]
    public class DummyController : ControllerBase
    {

        private readonly IQueueCommandDispatcher _dispatcher;
        private readonly IUserAccessor _userAccessor;

        public DummyController(IQueueCommandDispatcher dispatcher, IUserAccessor userAccessor)
        {
            _dispatcher = dispatcher;
            _userAccessor = userAccessor;
        }

        [HttpPost]
        [Route("command")]
        public async Task<AcceptedResult> Dummy(DummyRequest request)
        {
            await _dispatcher.HandleAsync(new DummyCommand { PastAsResponse = request.PastAsResponse });
            return Accepted();
        }

        [HttpPost]
        [Route("user")]
        [Authorize]
        public StatusCodeResult UserTest(DummyUserRequest request)
        {
            var user = _userAccessor.Get();
            if (user == null) return BadRequest();
            if (user.Login != request.Login) return BadRequest();
            if (user.Company != request.Company) return BadRequest();
            return Ok();
        }

    }
}
