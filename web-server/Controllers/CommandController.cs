using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reboard.CQRS;
using Reboard.WebServer.Architecture;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.WebServer.Controllers
{
    [Route("api/command")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly IQueueCommandDispatcher _dispatcher;
        private readonly IHttpContextAccessor _contextAccessor;

        public CommandController(IQueueCommandDispatcher dispatcher, IHttpContextAccessor contextAccessor)
        {
            _dispatcher = dispatcher;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var job = await _dispatcher.GetJob(id);
            switch (job.Status)
            {
                case CommandStatus.WaitingToRun:
                case CommandStatus.Running:
                    return this.AcceptedAtTask(id);

                case CommandStatus.Completed:
                    return Created(job.GetResourceUrl(), null);

                case CommandStatus.Faulted:
                    return Problem(title: "Command faulted", instance: id.ToString(), type: job.Command.GetType().FullName);

                default:
                    return Problem("Command status unknown", id.ToString(), (int?)job.Status);
            }
        }

        [HttpGet("ws")]
        public async Task<IActionResult> Ws()
        {
            if (!_contextAccessor.HttpContext.WebSockets.IsWebSocketRequest)
            {
                return BadRequest();
            }
            var webSocket = await _contextAccessor.HttpContext.WebSockets.AcceptWebSocketAsync();
            await Task.Factory.StartNew(async ()=>{
                while(webSocket.State == WebSocketState.Open) {
                    await Task.Delay(100);
                    var msgBytes = Encoding.UTF8.GetBytes("Hello world!");
                    await webSocket.SendAsync(new ArraySegment<byte>(msgBytes, 0, msgBytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }).ConfigureAwait(false);
            await WebSocketWaitLoop(webSocket);
            return StatusCode(101);
        }
private async Task WebSocketWaitLoop(WebSocket webSocket)
{
    var buffer = new byte[1024];
    while (webSocket.State.HasFlag(WebSocketState.Open))
    {
        try
        {
            var received =
                await webSocket.ReceiveAsync(buffer,CancellationToken.None);
        }
        catch(Exception ex)
        {
            break;
        }
    }

    if (webSocket.State != WebSocketState.Closed &&
        webSocket.State != WebSocketState.Aborted)
    {
        try
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                "Socket closed",
                CancellationToken.None);
        }
        catch
        {
            // this may throw on shutdown and can be ignored
        }
    }

}

    }
}