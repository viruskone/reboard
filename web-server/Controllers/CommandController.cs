using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.WebServer.Architecture;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/command")]
    [ApiController]
    [Authorize]
    public class CommandController : ControllerBase
    {
        private readonly IQueueCommandDispatcher _dispatcher;
        private readonly IWsProvider _wsProvider;

        public CommandController(IQueueCommandDispatcher dispatcher, IWsProvider wsProvider)
        {
            _dispatcher = dispatcher;
            _wsProvider = wsProvider;
        }

        [HttpGet("ws")]
        public async Task<IActionResult> Ws()
        {
            var acceptStatus = await _wsProvider.Accept();
            switch (acceptStatus)
            {
                case WebSocketAcceptStatus.ItsNotWebSocketRequest:
                    return BadRequest();
                case WebSocketAcceptStatus.Broken:
                    return StatusCode(519, "Connection broken");
                case WebSocketAcceptStatus.Closed:
                    return StatusCode(101);
                case WebSocketAcceptStatus.CannotAssignToUser:
                    return Unauthorized();
                default:
                    return StatusCode(101);
            }
        }
    }
}