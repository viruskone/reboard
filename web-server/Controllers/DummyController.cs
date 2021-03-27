using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.Domain.Dummy;
using Reboard.Domain.Dummy.Commands;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/dummy")]
    [ApiController]
    [Authorize]
    public class DummyController : ControllerBase
    {

        private readonly IQueueCommandDispatcher _dispatcher;

        public DummyController(IQueueCommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("command")]
        public async Task<AcceptedResult> Dummy(DummyRequest request)
        {
            await _dispatcher.HandleAsync(new DummyCommand { PastAsResponse = request.PastAsResponse });
            return Accepted();
        }

    }
}
